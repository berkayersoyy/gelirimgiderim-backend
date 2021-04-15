using System;

namespace Core.Utilities.RoomInvitation
{
    public class CodeGenerator:ICodeGenerator
    {
        public string Generate()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string code = "";
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                code += chars[random.Next(chars.Length)];
            }
            return code;
        }
    }
}