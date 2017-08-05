// Copyright (c) 2017 Cyotek Ltd.
// http://www.cyotek.com/blog/writing-custom-markdig-extensions
// Licensed under the MIT License. See LICENSE.txt for the full text.

using Markdig;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Renderers;

namespace MarkdigMantisLink
{
  public class MantisLinkerExtension : IMarkdownExtension
  {
    #region Constants

    private readonly MantisLinkOptions _options;

    #endregion

    #region Constructors

    public MantisLinkerExtension(MantisLinkOptions options)
    {
      _options = options;
    }

    #endregion

    #region IMarkdownExtension Interface

    public void Setup(MarkdownPipelineBuilder pipeline)
    {
      OrderedList<InlineParser> parsers;

      parsers = pipeline.InlineParsers;

      if (!parsers.Contains<MantisLinkInlineParser>())
      {
        parsers.Add(new MantisLinkInlineParser());
      }
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
      HtmlRenderer htmlRenderer;
      ObjectRendererCollection renderers;

      htmlRenderer = renderer as HtmlRenderer;
      renderers = htmlRenderer?.ObjectRenderers;

      if (renderers != null && !renderers.Contains<MantisLinkRenderer>())
      {
        renderers.Add(new MantisLinkRenderer(_options));
      }
    }

    #endregion
  }
}
