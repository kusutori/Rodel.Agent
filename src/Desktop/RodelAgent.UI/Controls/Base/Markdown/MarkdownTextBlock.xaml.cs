﻿// Copyright (c) Rodel. All rights reserved.
// <auto-generated />

using Markdig;
using RodelAgent.UI.Controls.Markdown.Renderers;
using RodelAgent.UI.Controls.Markdown.TextElements;

namespace RodelAgent.UI.Controls.Markdown;

[TemplatePart(Name = MarkdownContainerName, Type = typeof(Grid))]
public partial class MarkdownTextBlock : Control
{
    private const string MarkdownContainerName = "MarkdownContainer";
    private Grid _container;
    private readonly MarkdownPipeline _pipeline;
    private readonly MyFlowDocument _document;
    private WinUIRenderer _renderer;

    private static readonly DependencyProperty ConfigProperty = DependencyProperty.Register(
        nameof(Config),
        typeof(MarkdownConfig),
        typeof(MarkdownTextBlock),
        new PropertyMetadata(MarkdownConfig.Default, OnConfigChanged));

    private static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(MarkdownTextBlock),
        new PropertyMetadata(null, OnTextChanged));

    public MarkdownConfig Config
    {
        get => (MarkdownConfig)GetValue(ConfigProperty);
        set => SetValue(ConfigProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    private static void OnConfigChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is MarkdownTextBlock self && e.NewValue != null)
        {
            self.ApplyConfig(self.Config);
        }
    }

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is MarkdownTextBlock self && e.NewValue != null)
        {
            self.ApplyText(self.Text, true);
        }
    }

    public MarkdownTextBlock()
    {
        DefaultStyleKey = typeof(MarkdownTextBlock);
        _document = new MyFlowDocument();
        _pipeline = new MarkdownPipelineBuilder()
            .UseEmphasisExtras()
            .UseAutoLinks()
            .UseTaskLists()
            .UsePipeTables()
            .Build();
    }

    /// <inheritdoc/>
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _container = (Grid)GetTemplateChild(MarkdownContainerName);
        _container.Children.Clear();
        _container.Children.Add(_document.RichTextBlock);
        Build();
    }

    private void ApplyConfig(MarkdownConfig config)
    {
        if (_renderer != null)
        {
            _renderer.Config = config;
        }
    }

    private void ApplyText(string text, bool rerender)
    {
        var markdown = Markdig.Markdown.Parse(text, _pipeline);
        if (_renderer != null)
        {
            if (rerender)
            {
                _renderer.ReloadDocument();
            }

            _renderer.Render(markdown);
        }
    }

    private void Build()
    {
        if (Config != null)
        {
            _renderer ??= new WinUIRenderer(_document, Config);
            _pipeline.Setup(_renderer);
            ApplyText(Text, false);
        }
    }
}
