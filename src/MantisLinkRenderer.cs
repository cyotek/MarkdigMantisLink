// Copyright (c) 2017 Cyotek Ltd.
// http://www.cyotek.com/blog/writing-custom-markdig-extensions
// Licensed under the MIT License. See LICENSE.txt for the full text.

using Markdig.Helpers;
using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace MarkdigMantisLink
{
  public class MantisLinkRenderer : HtmlObjectRenderer<MantisLink>
  {
    #region Fields

    private MantisLinkOptions _options;

    #endregion

    #region Constructors

    public MantisLinkRenderer(MantisLinkOptions options)
    {
      _options = options;
    }

    #endregion

    #region Properties

    public MantisLinkOptions Options
    {
      get { return _options; }
      set { _options = value; }
    }

    #endregion

    #region Methods

    protected override void Write(HtmlRenderer renderer, MantisLink obj)
    {
      StringSlice issueNumber;

      issueNumber = obj.IssueNumber;

      if (renderer.EnableHtmlForInline)
      {
        // write a full a tag
        renderer.Write("<a href=\"").Write(_options.Url).Write("view.php?id=").Write(issueNumber).Write('"');

        if (_options.OpenInNewWindow)
        {
          // if adding the `target` attribute, also add a `rel` as per MDN
          // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/a#attr-target
          renderer.Write(" target=\"blank\" rel=\"noopener noreferrer\"");
        }

        renderer.Write('>').Write('#').Write(issueNumber).Write("</a>");
      }
      else
      {
        // inline HTML is disabled, so write the raw value
        renderer.Write('#').Write(obj.IssueNumber);
      }
    }

    #endregion
  }
}
