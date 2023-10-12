﻿using Phoenix.Core.Interfaces;
using System.Collections.Generic;

namespace Phoenix.Core.Interfaces
{
    internal interface IOptions
    {
        string InputPath { get; }
        string InputExtension { get; }
        string InputFileName { get; }
        string OutputPath { get; }
        string OutputFileName { get; }

        List<IStage> Stages { get; }
    }
}