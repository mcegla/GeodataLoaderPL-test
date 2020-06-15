﻿using GMLParserPL.Configuration;
using System;
using System.Dynamic;

namespace GMLParserPL.Translators
{
    internal class StandardTranslator : Translator
    {
        public StandardTranslator(string bdotClass, string filePath, Config config) : base(bdotClass, filePath, config)
        {
        }

        protected sealed override void Translate(ExpandoObject objectToTranslate)
        {
            Console.WriteLine($"{ObjectTypeEnum.Error};BDOT Class not implemented");
        }
    }
}
