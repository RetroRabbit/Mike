# Bouncer

Drop in intrusion detection and prevention for ASP.Net based web applications.

# Installation

Someday nuget will be available...

# Usage

## OWIN
```C#
public void Configuration(IAppBuilder app)
{
    app.UseBouncer();
}
```

## ASP.Net 5
Coming someday! In the meantime [do this](https://docs.asp.net/en/latest/fundamentals/owin.html).

## System.Web
```XML
<system.webServer>
	<modules>
	    ...
		<add name="Bouncer" type="Bouncer.SystemWeb.BouncerModule, Version=1.0.0.0" />
		...
	</modules>
</system.webServer>
```

# Configuration

## OWIN & ASP.Net 5 (someday)
```C#
public void Configuration(IAppBuilder app)
{
    var config = new BouncerConfiguration()
	{
		...
	};

    app.UseBouncer(config);
}
```

## System.Web
```C#
using Bouncer.SystemWeb;

public class Global : HttpApplication
{
    protected void Application_Start(object sender, EventArgs e)
    {
        BouncerModule.BouncerManager.Configuration = new BouncerConfiguration
        {
			...
        };
    }
}
```


# Customazation

Somewhere...

```C#
public class MyBouncer: BouncerManager
{
	...
}
```

## OWIN & ASP.Net 5 (someday)
```C#
public void Configuration(IAppBuilder app)
{
    var bouncer = new MyBouncer();

    app.UseBouncer(bouncer);
}
```

## System.Web
```C#
using Bouncer.SystemWeb;

public class Global : HttpApplication
{
    protected void Application_Start(object sender, EventArgs e)
    {
        BouncerModule.BouncerManager = new MyBouncer(); 
    }
}
```