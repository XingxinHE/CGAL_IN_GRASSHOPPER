﻿using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace CGAL.Grasshopper.Win
{
    public class CGAL_Grasshopper_WinInfo : GH_AssemblyInfo
    {
        public override string Name => "CGAL.Grasshopper.Win";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("93AB2F74-BBC6-4E06-B5E0-08332AF44DEB");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}