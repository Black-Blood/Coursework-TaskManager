using NUnit.Framework;

using BLL;
using System.IO;

namespace BLLTesting
{
    public class ProjectControllerTesting
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ProjectController_CreateProject_FileJson()
        {
            _ = new ProjectController("E:\\Store\\Test.json", "test", "test Des");

            FileAssert.Exists("E:\\Store\\Test.json");
        }

        [Test]
        public void ProjectController_OpenProject_FileJson()
        {
            Assert.DoesNotThrow(() =>
            {
                ProjectController controller = new("E:\\Store\\Test.json");
            });
        }

        [Test]
        public void ProjectController_CreateProject_FileXML()
        {
            _ = new ProjectController("E:\\Store\\Test.xml", "test", "test Des");

            FileAssert.Exists("E:\\Store\\Test.xml");
        }

        [Test]
        public void ProjectController_OpenProject_FileXML()
        {

            Assert.DoesNotThrow(() =>
            {
                ProjectController controller = new("E:\\Store\\Test.xml");
            });
        }

        [Test]
        public void ProjectController_CreateProject_FileDat()
        {
            _ = new ProjectController("E:\\Store\\Test.dat", "test", "test Des");

            FileAssert.Exists("E:\\Store\\Test.dat");
        }

        [Test]
        public void ProjectController_OpenProject_FileDat()
        {
            Assert.DoesNotThrow(() =>
            {

                ProjectController controller = new("E:\\Store\\Test.dat");
            });
        }

        [Test]
        public void ProjectController_GetProjectInfo_Properties()
        {
            ProjectController controller = new("E:\\Store\\Test1.json", "test", "test Des");

            ProjectView projectView = controller.GetProjectInfo();


            if (projectView.Name == "test" && projectView.Description == "test Des" && projectView.Progress == "0%")
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }


        [Test]
        public void ProjectController_ChangeTaskStatus_Done()
        {
            ProjectController controller = new("E:\\Store\\Test23.json", "test", "test Des");
            controller.taskController.CreateTask("Task1", "2021-11-12 15:00", "Des");

            controller.ChangeTaskStatus(0, "Done");

            bool condition = controller.searchController.FilterTaskBy(SearchController.FilterTaskType.Done).Count > 0;
            Assert.IsTrue(condition);
        }

        [Test]
        public void ProjectController_SetExecutant_Success()
        {
            ProjectController controller = new("E:\\Store\\Test23.json", "test", "test Des");
            controller.taskController.CreateTask("Task1", "2021-11-12 15:00", "Des");
            controller.teamMemberController.CreateTeamMember("Bohdan", "Savrak", "savrakbohdan@gmail.com");

            Assert.DoesNotThrow(() =>
            {
                controller.SetExecutant(0, 0);
            });
        }

        [Test]
        public void ProjectController_CheckLoadOfExecutants_Success()
        {
            ProjectController controller = new("E:\\Store\\Test33.json", "test", "test Des");
            controller.taskController.CreateTask("Task1", "2021-11-12 15:00", "Des");
            controller.teamMemberController.CreateTeamMember("Bohdan", "Savrak", "savrakbohdan@gmail.com");
            controller.SetExecutant(0, 0);

            LoadOfExecutant load = controller.CheckLoadOfExecutants()[0];
            bool condition = load.Count == "1";
            Assert.IsTrue(condition);
        }


    }
}