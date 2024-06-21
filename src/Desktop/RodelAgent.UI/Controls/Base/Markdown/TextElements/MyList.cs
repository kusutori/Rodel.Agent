﻿// Copyright (c) Rodel. All rights reserved.
// <auto-generated />

using System.Globalization;
using Markdig.Syntax;
using Microsoft.UI.Xaml.Documents;
using RomanNumerals;

namespace RodelAgent.UI.Controls.Markdown.TextElements;

internal class MyList : IAddChild
{
    private Paragraph _paragraph;
    private InlineUIContainer _container;
    private readonly StackPanel _stackPanel;
    private readonly ListBlock _listBlock;
    private readonly BulletType _bulletType;
    private readonly bool _isOrdered;
    private readonly int _startIndex = 1;
    private int _index = 1;
    private const string _dot = "•";

    public TextElement TextElement => _paragraph;

    public MyList(ListBlock listBlock)
    {
        _paragraph = new Paragraph();
        _container = new InlineUIContainer();
        _stackPanel = new StackPanel();
        _listBlock = listBlock;

        if (listBlock.IsOrdered)
        {
            _isOrdered = true;
            _bulletType = ToBulletType(listBlock.BulletType);

            if (listBlock.OrderedStart != null && (listBlock.DefaultOrderedStart != listBlock.OrderedStart))
            {
                _startIndex = int.Parse(listBlock.OrderedStart, NumberFormatInfo.InvariantInfo);
                _index = _startIndex;
            }
        }

        _stackPanel.Orientation = Orientation.Vertical;
        _container.Child = _stackPanel;
        _paragraph.Inlines.Add(_container);
    }

    public void AddChild(IAddChild child)
    {
        var grid = new Grid();
        grid.ColumnSpacing = 8;
        grid.Padding = new Thickness(0, 4, 0, 4);
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
        string bullet;
        if (_isOrdered)
        {
            bullet = _bulletType switch
            {
                BulletType.Number => $"{_index}. ",
                BulletType.LowerAlpha => $"{_index.ToAlphabetical()}. ",
                BulletType.UpperAlpha => $"{_index.ToAlphabetical().ToUpper()}. ",
                BulletType.LowerRoman => $"{_index.ToRomanNumerals().ToLower()} ",
                BulletType.UpperRoman => $"{_index.ToRomanNumerals().ToUpper()} ",
                BulletType.Circle => _dot,
                _ => _dot
            };
            _index++;
        }
        else
        {
            bullet = _dot;
        }

        var textBlock = new TextBlock()
        {
            Text = bullet,
        };
        textBlock.SetValue(Grid.ColumnProperty, 0);
        textBlock.VerticalAlignment = VerticalAlignment.Top;
        grid.Children.Add(textBlock);
        var flowDoc = new MyFlowDocument();
        flowDoc.AddChild(child);

        flowDoc.RichTextBlock.SetValue(Grid.ColumnProperty, 1);
        flowDoc.RichTextBlock.Padding = new Thickness(0);
        flowDoc.RichTextBlock.VerticalAlignment = VerticalAlignment.Top;
        grid.Children.Add(flowDoc.RichTextBlock);

        _stackPanel.Children.Add(grid);
    }

    private static BulletType ToBulletType(char bullet)
    {
        // Gets or sets the type of the bullet (e.g: '1', 'a', 'A', 'i', 'I').
        return bullet switch
        {
            '1' => BulletType.Number,
            'a' => BulletType.LowerAlpha,
            'A' => BulletType.UpperAlpha,
            'i' => BulletType.LowerRoman,
            'I' => BulletType.UpperRoman,
            _ => BulletType.Circle,
        };
    }
}

internal enum BulletType
{
    Circle,
    Number,
    LowerAlpha,
    UpperAlpha,
    LowerRoman,
    UpperRoman,
}
