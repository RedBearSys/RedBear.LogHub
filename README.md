<img src="https://raw.githubusercontent.com/RedBearSys/RedBear.LogHub/master/media/logo.png" width="300" />

An NLog-based bridge between remote applications and local debugging software using Azure Service Bus.

## Introduction
LogHub is a combination of an [NLog](http://nlog-project.org/) target and a system tray receiving application for Windows. Using the target, log-events are streamed to an [Azure Service Bus](https://azure.microsoft.com/en-gb/services/service-bus/) topic. The system tray application subscribes to that topic, deserialises the log event data and uses a local copy of NLog to forward the events to a target on your machine.

This allows you to use a local log viewing application - e.g. [Sentinel](http://sentinel.codeplex.com/) or [Log4View](http://www.log4view.com/log4view/) - with production applications running anywhere in the world.

<img src="https://raw.githubusercontent.com/RedBearSys/RedBear.LogHub/master/media/diagram.png" />

The benefits of using LogHub versus getting your production applications to connect directly to a port on your workstation include:

* unlimited users can monitor the same log at the same time without you needing to touch the production application's configuration;
* there's no need to punch holes in firewalls or map external ports to internal ports; developers can choose which logs to stream without IT involvement.

## Service Bus setup
You must first create a Service Bus to receive, store and forward your NLog log-events. Each application will be a topic within that Service Bus.

You can create this within the [Azure Portal](https://portal.azure.com/), or you can create your Service Bus within a Resource Group of your choice by [clicking here](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FAzure%2Fazure-quickstart-templates%2Fmaster%2F101-servicebus-create-namespace%2Fazuredeploy.json).

**Note:** in most cases you will only need *one* Service Bus for *all* your organisation's applications.

Topics can be created automatically via the NLog target. Topics that are created in this way will have a message time-to-live of 1 minute. If you need a longer time-to-live, create the topic manually within the [Azure Portal](https://portal.azure.com/).

## Logging setup in your originating application

Use NuGet to install the LogHub.Target package:

```
Install-Package LogHub.Target
```
Then [configure NLog](https://github.com/NLog/NLog/wiki/Tutorial#configuration) to use the LogHub Target. You should use a ```BufferingWrapper``` with the LogHub target as follows, specifying your Service Bus connection string and a topic name:

```xml
  <nlog>
    <extensions>
      <add assembly="LogHub.Target"/>
    </extensions>
    <targets>
      <target name="buffer" type="BufferingWrapper" bufferSize="100" flushTimeout="1000">
        <target name="lh" type="LogHubTarget" ConnectionString="Endpoint=sb://xxxx.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=xxxxyyyyzzz" Topic="myappname" />
      </target>
    </targets>
    <rules>
      <logger name="*" minLevel="Trace" writeTo="buffer"/>
    </rules>
  </nlog>
  ```

You should then create log entries in your application in the [usual way](https://github.com/nlog/nlog/wiki/Tutorial#writing-log-messages).

## Receiving log entries locally
Either download the code and build ```LogHub.exe``` yourself, or download and run the latest [installer](https://rbpublic.blob.core.windows.net/loghub/loghub-setup.msi).

LogHub will launch as a system tray application, so double-click the tray icon to display the main user interface.

You subscribe to a topic on your Service Bus by clicking "Add Source". You can specify the following fields:

* Topic name (mandatory);
* Logger Name Prefix (optional) - allows you to prepend a string to the logger name. This feature can make it easier to configure your local NLog rules when you want to work with multiple targets;
* Connection String (mandatory) - for your Service Bus;
* Enabled - to turn streaming of log entries on or off.

To configure how NLog deals with the received log entries, click the "Config" button on the main window. This will expose an NLog.xml file that you should edit in a text editor to [configure NLog's behaviour](https://github.com/NLog/NLog/wiki/Tutorial#configuration)  to your requirements.

## Not seeing any log entries on your local machine?
Firstly, check whether you're seeing anything in your local log output from LogHub itself. If LogHub  has run into any problems, it will output the details to your log using a logger name of "LogHub". **Note:** You may need to change/add to your NLog rules to capture events from this logger.

One of the most common issues is that LogHub can't reach the Azure Service Bus because of firewall restrictions or anti-virus / anti-malware software preventing ```LogHub.exe``` from accessing the Internet. This should get reported in your local log target if it's the case. Your firewall should allow workstations to access remote IP addresses within the 9350 to 9354 port range.

It could also be that your Topic or Connection String for the application is incorrect. Again, this should surface in your local log.

If you're *still* not seeing anything, double-check the rule elements in your NLog.xml file. We **strongly** recommend that you keep the log level at 'Trace' level (the lowest possible level). In that way, everything you receive *will* be logged. If you want to filter by log level, do this either at the *source* application (so LogHub doesn't receive lower-level entries to begin with) or filter in your *destination* viewing tool.
