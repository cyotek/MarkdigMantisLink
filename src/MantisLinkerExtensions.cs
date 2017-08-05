// Copyright (c) 2017 Cyotek Ltd.
// http://www.cyotek.com/blog/writing-custom-markdig-extensions
// Licensed under the MIT License. See LICENSE.txt for the full text.

using Markdig;
using Markdig.Helpers;

namespace MarkdigMantisLink
{
  public static class MantisLinkerExtensions
  {
    #region Static Methods

    public static MarkdownPipelineBuilder UseMantisLinks(this MarkdownPipelineBuilder pipeline, MantisLinkOptions options)
    {
      OrderedList<IMarkdownExtension> extensions;

      extensions = pipeline.Extensions;

      if (!extensions.Contains<MantisLinkerExtension>())
      {
        extensions.Add(new MantisLinkerExtension(options));
      }

      return pipeline;
    }

    #endregion
  }
}
