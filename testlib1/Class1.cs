﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEALibrary
{
    public class testclass1
    {
        public testclass1()
        {

        }

        public string class1test(String s)
        {
            return "Result from testlib1" + s;
        }
        public string class2test(String s, int a)
        {
            return "Result from testlib1" + s + a.ToString();
        }
    }
}