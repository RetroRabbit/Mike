# Mike

Drop in intrusion detection and prevention for .Net based web applications.

Features:
- [ ] Transparent and efficient intrusion detection
- [ ] Intrusion prevention via captcha challenge
- [ ] Throttling of high volume of requests from single IP Address
- [x] Load balancer IP Address forwarding and rewriting
- [ ] Low overhead
- [ ] Support for clustered environments
- [ ] Fluent configuration
- [ ] State storage and caching in Redis
- [ ] Rename to Mike
- [ ] MyGet continuous integration
- [ ] ASP.Net Core
- [ ] Fluent configuration
- [ ] Extensibility

# Installation

Someday nuget will be available...

# Usage

### OWIN
```C#
public void Configuration(IAppBuilder app)
{
    app.UseMike();
}
```

### ASP.Net Core
Coming someday! In the meantime [do this](https://docs.asp.net/en/latest/fundamentals/owin.html).

### System.Web
```XML
<system.webServer>
	<modules>
	    ...
		<add name="Mike" type="Mike.SystemWeb.MikeModule, Mike.SystemWeb, Version=1.0.0.0"/>
		...
	</modules>
</system.webServer>
```

# Configuration

### OWIN & ASP.Net 5 (someday)
```C#
public void Configuration(IAppBuilder app)
{
    var config = new MikeConfiguration()
	{
		...
	};

    app.UseMike(config);
}
```

### System.Web
```C#
using Mike.SystemWeb;

public class Global : HttpApplication
{
    protected void Application_Start(object sender, EventArgs e)
    {
        MikeModule.MikeIds.Configuration = new MikeConfiguration
        {
			...
        };
    }
}
```


# Customization

Somewhere...

```C#
public class MyMike: MikeIds
{
	...
}
```

### OWIN & ASP.Net Core (someday)
```C#
public void Configuration(IAppBuilder app)
{
    var Mike = new MyMike();

    app.UseMike(Mike);
}
```

### System.Web
```C#
using Mike.SystemWeb;

public class Global : HttpApplication
{
    protected void Application_Start(object sender, EventArgs e)
    {
        MikeModule.MikeIds = new MyMike(); 
    }
}
```
