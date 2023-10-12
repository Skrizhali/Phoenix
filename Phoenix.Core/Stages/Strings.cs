using System.Collections.Generic;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Phoenix.Core.Interfaces;


namespace Phoenix.Core.Stages
{
    internal class Strings : IStage
    {
        public bool IsDetected { get; private set; }

        public void Execute(IContext context)
        {
            context.Logger.Info("Searching String Decrypter.");

            foreach (TypeDef type in context.moduleDef.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody)
                        continue;

                    List<Instruction> newInstructions = new List<Instruction>(method.Body.Instructions);

                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                        {
                            if (method.Body.Instructions[i + 1].OpCode == OpCodes.Call)
                            {
                                string decryptedString = Decrypt.DecryptString(method.Body.Instructions[i].Operand.ToString());
                                context.Logger.Success($"Decrypting: {newInstructions[i].Operand}");
                                newInstructions[i].Operand = decryptedString;
                                newInstructions[i + 1].OpCode = OpCodes.Nop;
                                IsDetected = true;
                            }
                        }
                    }
                }
            }

            context.Logger.Success("Strings has been decrypted!");   
        }
    }

    public class Decrypt
    {
        public static string DecryptString(string inputString)
        {
            int stringLength = inputString.Length;
            char[] encryptedChars = new char[stringLength];

            for (int i = 0; i < encryptedChars.Length; i++)
            {
                char currentChar = inputString[i];
                byte firstXor = (byte)((int)currentChar ^ (stringLength - i));
                byte secondXor = (byte)((int)(currentChar >> 8) ^ i);

                encryptedChars[i] = (char)(((int)secondXor << 8) | (int)firstXor);
            }

            return string.Intern(new string(encryptedChars));
        }
    }
}