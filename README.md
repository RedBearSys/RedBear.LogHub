# LogHub
An Azure-NLog debugging bridge

## Introduction
LogHub allows you to use a local log viewing application - e.g. [Sentinel](http://sentinel.codeplex.com/) or [Log4View](http://www.log4view.com/log4view/) - with production applications running on Azure.

By using the LogHub [NLog](http://nlog-project.org/) target, log-events are streamed to an Azure Service Bus topic. LogHub subscribes to that topic, deserialises the log event data and uses a local copy of NLog to forward the events to a viewer of your choice.

## Service Bus setup
You must first create a Service Bus to receive, store and forward your NLog log-events. Each application will be a topic within that Service Bus.

You can create this within the [Azure Portal](https://portal.azure.com/), or you can create your Service Bus within a Resource Group of your choice by [clicking here](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FAzure%2Fazure-quickstart-templates%2Fmaster%2F101-servicebus-create-namespace%2Fazuredeploy.json).

**Note:** you only need one Service Bus for all your organisation's applications.

Topics can be created automatically via the NLog target. Topics that are created in this way will have a message time-to-live of 1 minute. If you need a longer time-to-live, create the topic manually within the [Azure Portal](https://portal.azure.com/).

## Logging setup

Either download the code and build it yourself, or download an [installer](https://rbpublic.blob.core.windows.net/loghub/loghub-setup.msi).

Add a reference to ```LogHub.Target.dll``` in your project and also add NLog (via Nuget).

You should use a ```BufferingWrapper``` with the LogHub target as follows, specifying your Service Bus connection string and a topic name:

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

You should then create log entries in your applicatio in the [usual way](https://github.com/nlog/nlog/wiki/Tutorial#writing-log-messages).

## Receiving log entries
Run LogHub from your start menu. It will launch as a system tray application, so double-click the tray icon to display the main user interface.

You subscribe to a topic on your Service Bus by clicking "Add Source". You can specify the following fields:

* Topic name - (mandatory);
* Logger Name Prefix - allows you to prepend a string to the logger name to aid management in your log viewer tool (optional);
* Connection String - for your Service Bus (mandatory);
* Enabled - to turn streaming of log entries on or off.

To configure how NLog deals with the received log entries, click the "Config" button on the main window. This will expose an NLog.xml file that you should edit in a text editor to [configure NLog's behaviour](https://github.com/NLog/NLog/wiki/Tutorial#configuration) according to your viewer's requirements.
