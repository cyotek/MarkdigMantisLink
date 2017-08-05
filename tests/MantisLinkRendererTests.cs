// Copyright (c) 2017 Cyotek Ltd.
// http://www.cyotek.com/blog/writing-custom-markdig-extensions
// Licensed under the MIT License. See LICENSE.txt for the full text.

using System;
using System.IO;
using Markdig;
using Markdig.Helpers;
using Markdig.Renderers;
using MarkdigMantisLink;
using NUnit.Framework;

namespace MarkdigMantisLinkTests
{
  [TestFixture]
  public class MantisLinkRendererTests
  {
    #region  Tests

    [Test]
    public void Html_with_braces()
    {
      this.RunTest("[#001753]", "[<a href=\"https://issues.example.com/view.php?id=001753\" target=\"blank\" rel=\"noopener noreferrer\">#001753</a>]");
    }

    [Test]
    public void Html_with_brackets()
    {
      this.RunTest("(#001753)", "(<a href=\"https://issues.example.com/view.php?id=001753\" target=\"blank\" rel=\"noopener noreferrer\">#001753</a>)");
    }

    [Test]
    public void Html_with_invalid_sequence()
    {
      this.RunTest("#1701-A", "#1701-A");
    }

    [Test]
    public void Html_with_leading_whitespace()
    {
      this.RunTest("Issue #001753", "Issue <a href=\"https://issues.example.com/view.php?id=001753\" target=\"blank\" rel=\"noopener noreferrer\">#001753</a>");
    }

    [Test]
    public void Html_with_OpenInNewWindow_test()
    {
      this.RunTest("#001753", "<a href=\"https://issues.example.com/view.php?id=001753\" target=\"blank\" rel=\"noopener noreferrer\">#001753</a>");
    }

    [Test]
    public void Html_with_trailing_whitespace()
    {
      this.RunTest("#001753 issue", "<a href=\"https://issues.example.com/view.php?id=001753\" target=\"blank\" rel=\"noopener noreferrer\">#001753</a> issue");
    }

    [Test]
    public void Html_without_OpenInNewWindow_test()
    {
      this.RunTest("#001753", "<a href=\"https://issues.example.com/view.php?id=001753\">#001753</a>", options => options.OpenInNewWindow = false);
    }

    [Test]
    public void Text_renderering_test()
    {
      // arrange
      MantisLinkRenderer target;
      MantisLinkOptions options;
      MantisLink link;
      TextWriter writer;
      HtmlRenderer renderer;
      string expected;
      string actual;

      // TODO: There's got to be an easier of testing plain text output

      expected = "#1338";

      writer = new StringWriter();
      renderer = new HtmlRenderer(writer)
                 {
                   EnableHtmlForInline = false
                 };

      options = new MantisLinkOptions("https://issues.example.com/");
      target = new MantisLinkRenderer(options);

      link = new MantisLink
             {
               IssueNumber = new StringSlice("1338", 0, 3)
             };

      // act
      target.Write(renderer, link);

      // assert
      actual = writer.ToString();
      Assert.AreEqual(expected, actual);
    }

    #endregion

    #region Test Helpers

    private void RunTest(string input, string expected)
    {
      this.RunTest(input, expected, null);
    }

    private void RunTest(string input, string expected, Action<MantisLinkOptions> setup)
    {
      // arrange
      MarkdownPipeline target;
      MantisLinkOptions options;
      string actual;

      options = new MantisLinkOptions("https://issues.example.com/");
      setup?.Invoke(options);

      target = new MarkdownPipelineBuilder().UseMantisLinks(options).Build();

      expected = "<p>" + expected + "</p>\n";

      // act
      actual = Markdown.ToHtml(input, target);

      // assert
      Assert.AreEqual(expected, actual);
    }

    #endregion
  }
}
