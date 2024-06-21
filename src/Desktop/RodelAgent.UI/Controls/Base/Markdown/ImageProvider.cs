﻿// Copyright (c) Rodel. All rights reserved.
// <auto-generated />

namespace RodelAgent.UI.Controls.Markdown;

public interface IImageProvider
{
    Task<Image> GetImage(string url);
    bool ShouldUseThisProvider(string url);
}
