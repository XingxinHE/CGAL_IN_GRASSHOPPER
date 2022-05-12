using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace CGAL.Grasshopper.Win
{
    public class Component_CGAL_Obb : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public Component_CGAL_Obb()
          : base("Oriented Bounding Box", "Obb",
            "Create an oriented bounding box with CGAL algorithm.",
            "Mesh", "Util")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "The mesh as input", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "P", "The 8 points of the bounding box.", GH_ParamAccess.list);
            pManager.AddBoxParameter("Box", "B", "The bounding box as box.", GH_ParamAccess.item);
            pManager.AddBrepParameter("Brep", "B", "The the bounding box as brep.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh mesh = new Mesh();
            if(!DA.GetData(0, ref mesh))
            {
                return;
            }

            var points = CGAL.Wrapper.PolygonMeshProcessing.ObbAsPoint3d(mesh);
            var box = CGAL.Wrapper.PolygonMeshProcessing.ObbAsBox(mesh);
            var brep = CGAL.Wrapper.PolygonMeshProcessing.ObbAsBrep(mesh);
            DA.SetDataList(0, points);
            DA.SetData(1, box);
            DA.SetData(2, brep);
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
        public override Guid ComponentGuid => new Guid("7FB173E4-DFE9-4F54-9CBD-E535E70E2906");
    }
}