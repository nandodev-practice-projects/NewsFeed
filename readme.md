---------------------------------
SAMPLE .NET CORE 3.1 WEB APPLICATION NEWSFEED
---------------------------------
Prerequisites and Installation Requirements

- .NET Core 3.1
- Visual Studio 2019

Instructions

- Clone this repository.
- Compile it.
- In order to use the SQL Broker, Service Broker for the database mus be enabled: The following command can be used: 
	ALTER DATABASE MyDatabase SET ENABLE_BROKER

- At hit F5 the database NewsFeed will be created and populated:
	Data base local connection server is in NewsFeed\appsettings.json:  Server=(localdb)\\ProjectsV13
	Please change as your local sql server name
	The users created are:

	username: user1
	password: abc@123
	role: Publisher

	username: user2
	password: abc@123
	role: Reader

- Execute the NewsFeed project and enter with the previous credentials. Use the command dotnet run

Functionalities:

1. Users can subscribe to a news feed.
2. Users can view items in a news feed.
3. Users can search for news feed items.
4. Users can see a listing of all news items from all feeds.
5. Users with the role "Publisher" can add a news

SignalR Funcionalities

Following the article http://elvanydev.com/SignalR-Core-SqlDependency-part2/ has been added the funcionalities of to emit
 notifications to client side on changes in the table News
 
 






