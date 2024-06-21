﻿// Copyright (c) Rodel. All rights reserved.
// <auto-generated />

using Markdig.Syntax.Inlines;
using RodelAgent.UI.Controls.Markdown.TextElements;

namespace RodelAgent.UI.Controls.Markdown.Renderers.ObjectRenderers.Inlines;

internal class LineBreakInlineRenderer : WinUIObjectRenderer<LineBreakInline>
{
    protected override void Write(WinUIRenderer renderer, LineBreakInline obj)
    {
        if (renderer == null)
        {
            throw new ArgumentNullException(nameof(renderer));
        }

        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        if (obj.IsHard)
        {
            renderer.WriteInline(new MyLineBreak());
        }
        else
        {
            // Soft line break.
            renderer.WriteText(" ");
        }
    }
}
