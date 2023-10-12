using Phoenix.Core.Interfaces;
using Phoenix.Core.Interfaces;
using Phoenix.Core;
using System;

namespace Phoenix.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Phoenix Unpacker by Team Skrizhali";
            var newLogger = new Logger();
            var newOptions = new Options(args);
            var context = new Context(newOptions, newLogger);

            newLogger.AsciiArt();

            if (context.ImportAssembly())
            {
                Deobfuscate(context);
                Export(context);
            }

            newLogger.Info("Press any key to exit.");
            Console.ReadKey();
        }

        private static void Deobfuscate(IContext context)
        {
            foreach (IStage stage in context.Options.Stages)
            {
                try
                {
                    stage.Execute(context);
                }
                catch (Exception ex)
                {
                    context.Logger.Error($"{stage.GetType().Name}: {ex.Message}");
                }
            }
        }

        private static void Export(IContext context) => context.ExportAssembly();
    }
}