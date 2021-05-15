# NotifyMemoryUsageInfo
.NET Core Windows Service By Using Worker Service 

# Introduction
It is a service that sends you an informtion mail according to the ram value you specified. You can specify the maximum ram value and windows service working time period in appsettings.json file.
It will log system ram status to the file path specified in the service configuration file every day at the specified time interval.

According to the memory value you set in the configuration file, it can send you e-mail via smtp at any time intervals.

In the attached configuration file, the service will run every 5 minutes and when it exceeds 10 GB of memory, it will send mail to the specified mail addresses. 
You can change all these settings in the configuration file. 

Before use the service do not forget install necessary packages. You can see all the necessary packages image below.

![packages](https://user-images.githubusercontent.com/50598737/117478359-b8b08300-af67-11eb-910a-46c3be986e82.png)

## Usage

Initially build the project and publish the project in a folder. After publishing, open your powershell as administrator and run the commands below.

```cmd
C:\Windows\System32\sc create NotifyMemoryUsageInfoService binPath=C:\Users\syoldas\Desktop\yourPublishFolder\NotifyMemoryUsageInfoService.exe
```
After that you will see the service in Services list. Then you can run the service.

![servicesimage](https://user-images.githubusercontent.com/50598737/117479989-aafbfd00-af69-11eb-8529-525209636d4c.png)

When the service starts running, it will start logging to the path you specified in the configuration file (C:\\Logs)
A new logging file will be created every day.


In the example below, the service has listened to the ram value in the system every 5 minutes and logged.

![calisanservis](https://user-images.githubusercontent.com/50598737/117481260-683b2480-af6b-11eb-87c5-597206f38dbf.png)

When there is a usage above the specified ram value in appsettings.json file, the log file will be as follows. And the mail will come as in the picture below.

![mailsend](https://user-images.githubusercontent.com/50598737/117483117-fa442c80-af6d-11eb-8154-015db6ca08f8.png)


Best Regards







