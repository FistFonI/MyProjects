#pragma comment(lib,"ws2_32.lib")
#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <stdio.h>
#include <winsock2.h>
#include <windows.h>
#include <string>
#include <thread>
#include <iostream>
#include <algorithm>
#include <sstream>
#include <map>

using namespace std;

#define UDPPORT 27015    // порт сервера
#define TCPPORT 27016

#define PRINTNUSERS if (nclients)\
  printf("%d user online\n",nclients);\
  else printf("No User online\n");

int nclients = 0;
map<SOCKET, string> clients;

static void GetLocalSetting() {
	setlocale(LC_ALL, "Russian");
}

int udpServer()
{
	char buff[80];

	printf("UDP Game Server\n");

	if (WSAStartup(0x202, (WSADATA*)&buff[0]))
	{
		printf("WSAStartup error: %d\n",
			WSAGetLastError());
		return -1;
	}

	SOCKET udp_sock;
	udp_sock = socket(AF_INET, SOCK_DGRAM, 0);
	if (udp_sock == INVALID_SOCKET)
	{
		printf("Socket() error: %d\n", WSAGetLastError());
		WSACleanup();
		return -1;
	}

	sockaddr_in udpAddress;
	udpAddress.sin_family = AF_INET;
	udpAddress.sin_addr.s_addr = INADDR_ANY;
	udpAddress.sin_port = htons(UDPPORT);


	if (bind(udp_sock, (sockaddr*)&udpAddress,
		sizeof(udpAddress))) {
		printf("bind error: %d\n", WSAGetLastError());
		closesocket(udp_sock);
		WSACleanup();
		return -1;
	}

	// Включение широковещательной рассылки
	bool broadcastEnable = true;
	if (setsockopt(udp_sock, SOL_SOCKET, SO_BROADCAST, (char*)&broadcastEnable, sizeof(broadcastEnable)) == SOCKET_ERROR)
	{
		std::cerr << "Error: setsockopt failed with error code " << WSAGetLastError() << std::endl;
		closesocket(udp_sock);
		WSACleanup();
		return 1;
	}

	string str;
	while (true)
	{
		sockaddr_in client_addr;
		int client_addr_size = sizeof(client_addr);

		int bsize = recvfrom(udp_sock, &buff[0],
			sizeof(buff) - 1, 0,
			(sockaddr*)&client_addr, &client_addr_size);
		if (bsize == SOCKET_ERROR) {
			printf("recvfrom() error: %d\n",
				WSAGetLastError());
		}


		HOSTENT* hst;
		hst = gethostbyaddr((char*)
			&client_addr.sin_addr, 4, AF_INET);
		printf("+%s [%s:%d] new DATAGRAM!\n",
			(hst) ? hst->h_name : "Unknown host",
			inet_ntoa(client_addr.sin_addr),
			ntohs(client_addr.sin_port));

		// добавление завершающего нуля
		buff[bsize] = 0;

		// Вывод на экран 
		printf("Принято UDP сообщение: %s\n", &buff[0]);

		string infoMessage = to_string(nclients);
		if (!strcmp(&buff[0], "9game"))
		{
			client_addr.sin_addr.s_addr = inet_addr(inet_ntoa(client_addr.sin_addr));
			int sendResult = sendto(udp_sock, infoMessage.c_str(), infoMessage.size(), 0, (SOCKADDR*)&client_addr, sizeof(client_addr));
			printf("Отправлен ответ.\n");
		}
	}

	return 0;
}

int TCPServer()
{
	printf("TCP Game Server\n");

	WSADATA wsData;
	WORD ver = MAKEWORD(2, 2);

	int wsOk = WSAStartup(ver, &wsData);
	if (wsOk != 0)
	{
		cerr << "Can't Initialize winsock! Quitting" << endl;
		return 99;
	}

	SOCKET listening = socket(AF_INET, SOCK_STREAM, 0);
	if (listening == INVALID_SOCKET)
	{
		cerr << "Can't create a socket! Quitting" << endl;
		return 99;
	}

	sockaddr_in hint;
	hint.sin_family = AF_INET;
	hint.sin_port = htons(TCPPORT);
	hint.sin_addr.S_un.S_addr = INADDR_ANY; 

	bind(listening, (sockaddr*)&hint, sizeof(hint));

	listen(listening, SOMAXCONN);

	fd_set master;
	FD_ZERO(&master);

	FD_SET(listening, &master);

	bool running = true;

	while (running)
	{
		fd_set copy = master;

		int socketCount = select(0, &copy, nullptr, nullptr, nullptr);

		for (int i = 0; i < socketCount; i++)
		{
			SOCKET sock = copy.fd_array[i];

			if (sock == listening)
			{ 
				sockaddr_in client_addr;   
				int client_addr_size = sizeof(client_addr);

				SOCKET client = accept(listening, (sockaddr*)&client_addr, &client_addr_size);

				FD_SET(client, &master);

				HOSTENT* hst = gethostbyaddr((char*)
					&client_addr.sin_addr.s_addr, 4, AF_INET);


				clients[client] = "";

				nclients++;

				printf("+%s [%s] новое TCP подключение!\n",
					(hst) ? hst->h_name : "",
					inet_ntoa(client_addr.sin_addr));
				PRINTNUSERS

				string welcomeMsg = "SERVER: Welcome to the server! Chat with people. Type \\help to get commands.";

				send(client, welcomeMsg.c_str(), welcomeMsg.size() + 1, 0);
			}
			else
			{
				char buf[4096];
				ZeroMemory(buf, 4096);

				int bytesIn = recv(sock, buf, 4096, 0);
				if (bytesIn <= 0)
				{
					closesocket(sock);
					FD_CLR(sock, &master);

					nclients--;
					printf("-disconnect\n"); PRINTNUSERS
				}
				else
				{
					if (buf[0] == '\\')
					{
						string cmd = string(buf, bytesIn);

						if (cmd == "\\help")
						{
							string help = "Available commands: \\help, \\name.";
							send(sock, help.c_str(), help.size() + 1, 0);
						}

						if (cmd.find("\\name") != string::npos)
						{
							size_t found = cmd.find("\\name");
							if (cmd.size() > 6)
							{
								string substr = cmd.substr(found + 6);
								clients[sock] = substr;
							}
							else 
							{
								string help = "Write the name what you want. E.g. \\name Anton";
								send(sock, help.c_str(), help.size() + 1, 0);
							}
						}

						continue;
					}

					for (u_int i = 0; i < master.fd_count; i++)
					{
						SOCKET outSock = master.fd_array[i];
						if (outSock == listening)
						{
							continue;
						}

						ostringstream ss;

						if (outSock != sock)
						{
							if (clients[sock].empty())
							{
								ss << "SOCKET #" << sock << ": " << buf << "\r\n";
							}
							else {
								ss << clients[sock] << ": " << buf << "\r\n";
							}
						}
						else
						{
							ss << "ME: " << buf << "\r\n";
						}

						string strOut = ss.str();
						send(outSock, strOut.c_str(), strOut.size() + 1, 0);
					}
				}
			}
		}
	}

	FD_CLR(listening, &master);
	closesocket(listening);

	string msg = "SERVER:Server is shutting down. Goodbye\r\n";

	while (master.fd_count > 0)
	{
		SOCKET sock = master.fd_array[0];

		send(sock, msg.c_str(), msg.size() + 1, 0);

		FD_CLR(sock, &master);
		closesocket(sock);
	}

	WSACleanup();

	system("pause");
	return 0;
}

int main(int argc, char* argv[])
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	system("color F0");

	GetLocalSetting();

	std::thread udpThread(udpServer);
	std::thread tcpThread(TCPServer);


	udpThread.join();
	tcpThread.join();

	return 0;
}