using CGAL.Wrapper;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace CGAL.Grasshopper.Win
{
    public class Component_CGAL_obb : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public Component_CGAL_obb()
          : base("Oriented Bounding Box", "Obb",
            "This is the oriented bounding box using CGAL algorithm.",
            "Mesh", "Util")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "The input for oriented bounding box is a mesh.", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "P", "The 8 points of the bounding box.", GH_ParamAccess.list);
            pManager.AddBoxParameter("Box", "B", "The bounding box as a box.", GH_ParamAccess.item);
            pManager.AddBrepParameter("Brep", "B", "The bounding box as a brep.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh mesh = new Mesh();
            if (!DA.GetData(0, ref mesh))
            {
                return;
            }

            DA.SetDataList(0, PolygonMeshProcessing.ObbAsPoint3d(mesh));
            DA.SetData(1, PolygonMeshProcessing.ObbAsBox(mesh));
            DA.SetData(2, PolygonMeshProcessing.ObbAsBrep(mesh));
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => null;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("05EA4480-D89B-4E4F-A422-41AA5C0C18DA");
    }
}