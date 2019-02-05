using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceWars;

/// <summary>
/// Tests for testing the functionality of the world.
/// Authors: Pranav Rajan, Thomas Ady
/// Date: December 4, 2018
/// </summary>
namespace WorldUnitTesting
{
    [TestClass]
    public class WorldTests
    {
        [TestMethod]
        public void TestConstructor1()
        {
            new World(500);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestConstructor1NegativeSize()
        {
            new World(-1);
        }

        [TestMethod]
        public void TestConstructor2()
        {
            new World(500, 5, 15, 1, 1, 1, 1, 1, 1, false);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestConstructor2NegativeSize()
        {
            new World(-500, 5, 15, 1, 1, 1, 1, 1, 1, false);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestConstructor2NegativeHP()
        {
            new World(500, -5, 15, 1, 1, 1, 1, 1, 1, false);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestConstructor2NegativeShipCircle()
        {
            new World(500, 5, 15, 1, 1, -1, 1, 1, 1, false);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestConstructor2NegativeStarCircle()
        {
            new World(500, 5, 15, 1, 1, 1, -1, 1, 1, false);
        }

        [TestMethod]
        public void TestConstructor2ExtraFeatureEnabled()
        {
            new World(500, 5, 15, 1, 1, 1, 1, 1, 1, true);
        }

        [TestMethod]
        public void TestSizeProperty()
        {
            World w = new World(500);
            Assert.AreEqual(500, w.Size);
        }

        [TestMethod]
        public void TestAddShip()
        {
            World w = new World(500);
            Assert.IsTrue(w.getShips().Count == 0);
            w.AddShip(new Ship());
            Assert.IsFalse(w.getShips().Count == 0);
        }

        [TestMethod]
        public void TestAddShipWithDifferentHP()
        {
            World w = new World(500, 100, 0, 0, 1, 1, 1, 1, 1, false);
            w.AddShip(new Ship());
            Assert.IsTrue(w.getShips()[0].HP == 100);
        }

        [TestMethod]
        public void TestAddShipWithExtraContentEnabled()
        {
            World w = new World(500, 100, 0, 0, 1, 1, 1, 1, 1, true);
            w.AddShip(new Ship(0, new Vector2D(), new Vector2D(), "Bob", 0));
            Assert.IsTrue(w.getShips()[0].Name.Equals("Bob, Team 1"));

            w.AddShip(new Ship(1, new Vector2D(), new Vector2D(), "Tom", 0));
            Assert.IsTrue(w.getShips()[1].Name.Equals("Tom, Team 2"));
        }

        [TestMethod]
        public void TestAddStar()
        {
            World w = new World(500);
            Assert.IsTrue(w.getStars().Count == 0);
            w.AddStar(new Star());
            Assert.IsFalse(w.getStars().Count == 0);
        }

        [TestMethod]
        public void TestAddProjectile()
        {
            World w = new World(500);
            Assert.IsTrue(w.getProjectiles().Count == 0);
            w.AddProjectile(new Projectile());
            Assert.IsFalse(w.getProjectiles().Count == 0);
        }

        [TestMethod]
        public void TestRemoveShip()
        {
            World w = new World(500);
            w.AddShip(new Ship(0, new Vector2D(), new Vector2D(), "wow", 0));
           Assert.IsTrue( w.RemoveShip(0));
            Assert.IsTrue(w.getShips().Count == 0);
        }

        [TestMethod]
        public void TestRemoveShipNoShip()
        {
            World w = new World(500);
            w.AddShip(new Ship(1, new Vector2D(), new Vector2D(), "wow", 0));
            Assert.IsFalse(w.RemoveShip(0));
            Assert.IsTrue(w.getShips().Count == 1);
        }

        [TestMethod]
        public void TestRemoveStar()
        {
            World w = new World(500);
            w.AddStar(new Star(0, new Vector2D(), 0));
            Assert.IsTrue(w.RemoveStar(0));
            Assert.IsTrue(w.getStars().Count == 0);
        }

        [TestMethod]
        public void TestRemoveStarNoStar()
        {
            World w = new World(500);
            w.AddStar(new Star(1, new Vector2D(), 0));
            Assert.IsFalse(w.RemoveStar(0));
            Assert.IsTrue(w.getStars().Count == 1);
        }

        [TestMethod]
        public void TestRemoveProjectile()
        {
            World w = new World(500);
            w.AddProjectile(new Projectile(0, new Vector2D(), new Vector2D(), 0));
            Assert.IsTrue(w.RemoveProjectile(0));
            Assert.IsTrue(w.getProjectiles().Count == 0);
        }

        [TestMethod]
        public void TestRemoveProjectileNoProjectile()
        {
            World w = new World(500);
            w.AddProjectile(new Projectile(1, new Vector2D(), new Vector2D(), 0));
            Assert.IsFalse(w.RemoveProjectile(0));
            Assert.IsTrue(w.getProjectiles().Count == 1);
        }

        [TestMethod]
        public void TestGetShip()
        {
            World w = new World(500);
            w.AddShip(new Ship(1, new Vector2D(), new Vector2D(), "wow", 0));
            Assert.IsTrue(w.getShip(1) != null);
        }

        [TestMethod]
        public void TestGetShipNoShip()
        {
            World w = new World(500);
            w.AddShip(new Ship(1, new Vector2D(), new Vector2D(), "wow", 0));
            Assert.IsTrue(w.getShip(0) == null);
        }

        [TestMethod]
        public void TestGetStar()
        {
            World w = new World(500);
            w.AddStar(new Star(0, new Vector2D(), 0));
            Assert.IsTrue(w.getStar(0) != null);
        }

        [TestMethod]
        public void TestGetStarNoStar()
        {
            World w = new World(500);
            w.AddStar(new Star(0, new Vector2D(), 0));
            Assert.IsTrue(w.getStar(1) == null);
        }

        [TestMethod]
        public void TestGetProjectile()
        {
            World w = new World(500);
            w.AddProjectile(new Projectile(1, new Vector2D(), new Vector2D(), 0));
            Assert.IsTrue(w.getProjectile(1) != null);
        }

        [TestMethod]
        public void TestGetProjectileNoProjectile()
        {
            World w = new World(500);
            w.AddProjectile(new Projectile(1, new Vector2D(), new Vector2D(), 0));
            Assert.IsTrue(w.getProjectile(0) == null);
        }

        [TestMethod]
        public void TestRemovePlayer()
        {
            World w = new World(500, 100, 0, 0, 1, 1, 1, 1, 1, false);
            w.AddShip(new Ship(1, new Vector2D(), new Vector2D(), "wow", 0));
            Assert.IsFalse(w.getShip(1).HP == 0);
            Assert.IsFalse(w.getShip(1).RespawnFrames == -1);
            w.RemovePlayer(1);
            Assert.IsTrue(w.getShip(1).HP == 0);
            Assert.IsTrue(w.getShip(1).RespawnFrames == -1);
        }

        [TestMethod]
        public void TestRemovePlayerNoPlayer()
        {
            World w = new World(500);
            w.AddShip(new Ship(1, new Vector2D(), new Vector2D(), "wow", 0));
            w.RemovePlayer(0); //This method just returns if the ID doesn't exist
        }

        [TestMethod]
        public void TestGenerateRandomSafeSpawn()
        {
            World w = new World(500, 100, 0, 0, 1, 1, 1, 1, 1, false);
            Assert.IsFalse(w.GenerateRandomSafeSpawn() == null);
        }

        [TestMethod]
        public void TestGenerateRandomSafeSpawnWithStar()
        {
            World w = new World(500, 100, 0, 0, 1, 1, 225, 1, 1, false);
            w.AddStar(new Star(0, new Vector2D(0, 0), 1));

            //Simulate spawn 100 times so that the spawn might try to go within star bounds and then retry generation
            for (int i = 0; i < 100; i++)
                w.GenerateRandomSafeSpawn();
        }

        [TestMethod]
        public void SimulateWorldRunning()
        {
            World w = new World(500, 1, 5, 0.08, 50, 75, 30, 5, 1, false);

            //Add one star and ship to the world
            w.AddStar(new Star(0, new Vector2D(0, 0), 0.05));
            w.AddShip(new Ship(0, new Vector2D(0, 0), new Vector2D(0, 1), "Tom", 0));

            Assert.IsTrue(w.getShip(0).HP == 1);

            //Simulate the world running
            w.ApplyWorldChanges();

            //Ship should have collided with the sun
            Assert.IsTrue(w.getShip(0).HP == 0);

            //Try to shoot while dead
            w.EnterCommands(0, "(F)\n");
            w.ApplyWorldChanges();
            Assert.IsTrue(w.getProjectiles().Count == 0);

            //Ship should respawn next frame
            Assert.IsFalse(w.getShip(0).HP == 0);
            Assert.IsFalse(w.getShip(0).Location.GetX() == 0);
            Assert.IsFalse(w.getShip(0).Location.GetY() == 0);

            //Simulate turn, thrust, fire, and attempt to fire while unable to fire
            w.EnterCommands(0, "(LTF)\n");
            w.ApplyWorldChanges();
            Assert.IsTrue(w.getProjectiles().Count == 1);
            w.EnterCommands(0, "(RTF)\n");
            w.ApplyWorldChanges();
            //Another couldn't be fired
            Assert.IsTrue(w.getProjectiles().Count == 1);
        }

        [TestMethod]
        public void SimulateWorldRunningWraparound()
        {
            World w = new World(500, 1, 50, 0.08, 50, 75, 100, 5, 1, false);
            w.AddShip(new Ship(0, new Vector2D(100, -250), new Vector2D(0, -1), "Tom", 0));
            w.AddShip(new Ship(1, new Vector2D(100, 250), new Vector2D(0, 1), "Tom2", 0));
            w.AddShip(new Ship(2, new Vector2D(-250, 100), new Vector2D(-1, 0), "Tom3", 0));
            w.AddShip(new Ship(3, new Vector2D(250, 100), new Vector2D(1, 0), "Tom4", 0));

            w.EnterCommands(0, "(T)\n");
            w.EnterCommands(1, "(T)\n");
            w.EnterCommands(2, "(T)\n");
            w.EnterCommands(3, "(T)\n");

            w.ApplyWorldChanges();

            w.EnterCommands(0, "(T)\n");
            w.EnterCommands(1, "(T)\n");
            w.EnterCommands(2, "(T)\n");
            w.EnterCommands(3, "(T)\n");

            w.ApplyWorldChanges();

            //Ships should have wrapped to the other side of the world
            Assert.IsFalse(w.getShip(0).Location.GetY() < 0);
            Assert.IsFalse(w.getShip(1).Location.GetY() > 0);
            Assert.IsFalse(w.getShip(2).Location.GetX() < 0);
            Assert.IsFalse(w.getShip(3).Location.GetX() > 0);

        }

        [TestMethod]
        public void TestProjectileSunCollisions()
        {
            World w = new World(500, 1, 50, 0.08, 50, 75, 40, 5, 1, false);

            //Add one star and ship to the world
            w.AddStar(new Star(0, new Vector2D(0, 0), 0.05));
            w.AddShip(new Ship(0, new Vector2D(-100, 0), new Vector2D(1, 0), "Tom", 0));
            w.EnterCommands(0, "(F)\n");
            w.ApplyWorldChanges();
            Assert.IsTrue(w.getProjectiles().Count == 1);

            //Projectile should hit sun on this frame
            w.ApplyWorldChanges();
            Assert.IsTrue(w.getProjectiles().Count == 0);
        }

        [TestMethod]
        public void SimulateWorldRunningExtraFeaturesEnabled()
        {
            World w = new World(500, 1, 60, 0.08, 50, 75, 150, 5, 10, true);
            w.AddShip(new Ship(0, new Vector2D(-50, 0), new Vector2D(1, 0), "Tom", 0));
            w.AddShip(new Ship(1, new Vector2D(50, 0), new Vector2D(-1, 0), "Bob", 0));

            w.EnterCommands(0, "(F)\n");
            w.EnterCommands(1, "(F)\n");
            w.ApplyWorldChanges();

            //Projectiles should collide
            Assert.IsTrue(w.getProjectiles().Count == 0);

            w.EnterCommands(0, "(F)\n");
            w.ApplyWorldChanges();

            Assert.IsTrue(w.getShip(1).HP == 0);
            Assert.IsTrue(w.getShip(0).Score == 1);
        }

        [TestMethod]
        public void TestSunGravityOnProjectiles()
        {
            World w = new World(500, 1, 0, 0.08, 50, 75, 10, 5, 10, true);
            w.AddStar(new Star(0, new Vector2D(0, 0), 0.1));
            w.AddShip(new Ship(0, new Vector2D(-50, 0), new Vector2D(1, 0), "Tom", 0));
            w.EnterCommands(0, "(F)\n");

            w.ApplyWorldChanges();

            //Projectile should move even with 0 velocity initially
            Assert.IsFalse(w.getProjectile(0).Location.GetX() - 100 > 0.001);
        }

        [TestMethod]
        public void TestProjectileWrapAround()
        {
            World w = new World(500, 1, 50, 0.08, 50, 75, 100, 5, 1, true);
            w.AddShip(new Ship(0, new Vector2D(50, -250), new Vector2D(0, -1), "Tom", 0));
            w.AddShip(new Ship(1, new Vector2D(100, 250), new Vector2D(0, 1), "Tom2", 0));
            w.AddShip(new Ship(2, new Vector2D(-250, 50), new Vector2D(-1, 0), "Tom3", 0));
            w.AddShip(new Ship(3, new Vector2D(250, 100), new Vector2D(1, 0), "Tom4", 0));

            w.EnterCommands(0, "(F)\n");
            w.EnterCommands(1, "(F)\n");
            w.EnterCommands(2, "(F)\n");
            w.EnterCommands(3, "(F)\n");

            w.ApplyWorldChanges();

            //Ships should have wrapped to the other side of the world
            Assert.IsFalse(w.getProjectile(0).Location.GetY() < 0);
            Assert.IsFalse(w.getProjectile(1).Location.GetY() > 0);
            Assert.IsFalse(w.getProjectile(2).Location.GetX() < 0);
            Assert.IsFalse(w.getProjectile(3).Location.GetX() > 0);

        }

        [TestMethod]
        public void TestProjectileHitScreen()
        {
            World w = new World(500, 1, 50, 0.08, 50, 75, 100, 5, 1, false);
            w.AddShip(new Ship(0, new Vector2D(50, -250), new Vector2D(0, -1), "Tom", 0));
            w.AddShip(new Ship(1, new Vector2D(100, 250), new Vector2D(0, 1), "Tom2", 0));
            w.AddShip(new Ship(2, new Vector2D(-250, 50), new Vector2D(-1, 0), "Tom3", 0));
            w.AddShip(new Ship(3, new Vector2D(250, 100), new Vector2D(1, 0), "Tom4", 0));

            w.EnterCommands(0, "(F)\n");
            w.EnterCommands(1, "(F)\n");
            w.EnterCommands(2, "(F)\n");
            w.EnterCommands(3, "(F)\n");

            w.ApplyWorldChanges();

            //Ships should have wrapped to the other side of the world
            Assert.IsTrue(w.getProjectiles().Count == 0);

        }

        [TestMethod]
        public void ProjectileCollidesWithProjectile()
        {
            World w = new World(500, 1, 10, 0.08, 5, 5, 100, 5, 1, true);
            w.AddShip(new Ship(0, new Vector2D(20, 0), new Vector2D(-1, 0), "Tom", 0));
            w.AddShip(new Ship(1, new Vector2D(-20, 0), new Vector2D(1, 0), "Tom2", 0));

            w.EnterCommands(0, "(F)\n");
            w.EnterCommands(1, "(F)\n");

            w.ApplyWorldChanges();
            w.ApplyWorldChanges();
            //Ships should have wrapped to the other side of the world
            Assert.IsTrue(w.getProjectiles().Count == 0);
            Assert.IsTrue(w.getShip(0).HP != 0);

        }
    }




}
