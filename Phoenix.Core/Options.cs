﻿using Phoenix.Core.Interfaces;
using Phoenix.Core.Interfaces;
using System.Collections.Generic;
using System.IO;
using System;
using Phoenix.Core.Stages;

namespace Phoenix.Core
{
    internal class Options : IOptions
    {
        public Options(IEnumerable<string> args)
        {
            string tempPath = null;

            foreach (var arg in args)
            {
                if (File.Exists(arg))
                {
                    tempPath = arg;
                    break;
                }
            }

            if (tempPath != null)
            {
                InputPath = Path.GetFullPath(tempPath);
                InputFileName = Path.GetFileName(InputPath);
                InputExtension = Path.GetExtension(InputPath);
                OutputPath =
                    Path.Combine(Path.GetFullPath(Path.GetDirectoryName(InputPath) ??
                                                  throw new InvalidOperationException()));
                OutputFileName = $"{InputFileName}_Skrizhali{InputExtension}";
            }
        }

        public string InputPath { get; }
        public string InputFileName { get; }
        public string InputExtension { get; }
        public string OutputPath { get; }
        public string OutputFileName { get; }

        public List<IStage> Stages { get; } = new List<IStage>()
        {
            new Strings()
        };
    }
}