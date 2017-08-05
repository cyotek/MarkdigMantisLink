Markdig MantisBT Linker
=======================

This is a Markdig extension that automatically add links to Mantis Bug Tracker issue items within your markdown, e.g. #001753.

Getting Started
---------------

* Build & reference the library
* Add the extension to your pipeline using the extension method `UseMantisLinks`
* Set your MantisBT base URL within the options
* All done!

```
var pipeline = new MarkdownPipelineBuilder()
  .UseMantisLinks(new MantisLinkOptions("https://issues.company.net/")
  .Build();
```

For an overview on how to build Markdig extensions using this library as an example, see the "[Writing custom Markdig extensions](https://www.cyotek.com/blog/writing-custom-markdig-extensions)" article on the cyotek blog.

Authors
-------

* [Richard Moss](https://richardmoss.name)

Acknowledgements
----------------

This project was heavily inspired by [Dave Clarke](https://daveclarke.me)'s [MarkdigJiraLinker](https://github.com/clarkd/MarkdigJiraLinker). 

License
-------

This project is licensed under the MIT license. See the LICENSE.txt for details.

Contributing
------------

Please feel free to raise pull requests or issues for any issues you find. 

