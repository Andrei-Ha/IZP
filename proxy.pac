function FindProxyForURL(url, host)
 {

 if (shExpMatch(host, "*.pes.*") || shExpMatch(host, "*10.181.64.*") || shExpMatch(host, "*pirr*"))
      return "PROXY 10.181.0.2:";
     else if (shExpMatch(host, "*energo*") || shExpMatch(host, "*kaskad*"))
          return "PROXY 10.181.64.3:55555";
		  else
		  return "PROXY 10.181.64.5:3128";
 }