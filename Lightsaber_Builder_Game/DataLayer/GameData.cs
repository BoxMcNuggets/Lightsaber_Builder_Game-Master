using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lightsaber_Builder_Game.Models;
using System.Collections.ObjectModel;
using Lightsaber_Builder_Game.Presentation;

namespace Lightsaber_Builder_Game.DataLayer
{
    public static class GameData
    {
        public static Player PlayerData()
        {
            return new Player()
            {
                Id = 1,
                Name = "Luke",
                Age = 19,
                JobTitle = Player.ForceSide.LightSide,
                Race = Character.RaceType.Human,
                Health = 100,
                Lives = 3,
                Inventory = new ObservableCollection<GameItemModel>()
                {
                    GameItemModelById(1002),
                    GameItemModelById(2001)
                }
            };
        }
        public static Map GameMap() 
        {
            Map gamemap = new Map();

            gamemap.Locations.Add
            (new Location()
                {
                    ID = 1,
                    Name = "Ship",
                    PlanetColor = "gray",
                    Description = "Your ship is a Tie-Fighter" +
                    "It is a weaponless, workmanlike Cruiser",
                    Accessible = true,
                    Message = "Welcome to your ship, From your ship you can travel to other places. Dont forget " +
                    "to grab your Blaster",
                    PlanetLog = "",
                    GameItems = new ObservableCollection<GameItemModel>
                    {
                        GameItemModelById(40),
                        GameItemModelById(10),
                        GameItemModelById(11)
                    }
                }
            );
            gamemap.Locations.Add
                (new Location()
                    {
                        ID = 2,
                        Name = "Coruscant",
                        PlanetColor = "/Lightsaber_Builder_Game;component/Graphics/Coruscant.jpg",
                        Description = "Coruscant is a city-covered planet with a population of one trillion beings." +
                        " Coruscant's surface was defined by its urban sprawl, which collectively was called Galactic City",
                        Accessible = true,
                        Message = "The city covered planet, It might be usefull to do some shopping while your here",
                        PlanetLog = "Once you have most of the lightsaber parts come back here to get your energy core",
                        GameItems = new ObservableCollection<GameItemModel>
                            {
                                GameItemModelById(34),
                                GameItemModelById(20)
                            }
                        }
                );
            gamemap.Locations.Add
                (new Location()
                    {
                        ID = 3,
                        Name = "Hoth",
                        PlanetColor = "/Lightsaber_Builder_Game;component/Graphics/Hoth.png",
                        Description = "Hoth is the sixth planet in the remote system of the same name, and was the site of the"+
                        " Rebel Alliance's Echo Base. It is a world of snow and ice, surrounded by numerous moons, and home to"+
                        " deadly creatures like the wampa.",
                        Accessible = true,
                        Message = "Hoth is a frozen winter planet with lots of life, but be careful because not everything is friendly",
                        PlanetLog = "Wampas are notorious on the planet hoth, im sure you could find some lightsaber parts in one of their caves",
                    GameItems = new ObservableCollection<GameItemModel>
                    {
                        GameItemModelById(31),
                        GameItemModelById(20),
                        GameItemModelById(10),
                        GameItemModelById(11)
                    }
                }
                );
            gamemap.Locations.Add
                (new Location()
                    {
                        ID = 4,
                        Name = "Mustafar",
                        PlanetColor = "/Lightsaber_Builder_Game;component/Graphics/mustafar.jpg",
                        Description = "Once a garden world nourished by the Bright Star artifact, its orbit was shifted when "+
                        "Lady Corvax unleashed the energies of the Bright Star in an attempt to return her husband to life. "+
                        "The resulting gravimetric duel between the gas giants Jestefad and Lefrani over Mustafar heated the "+
                        "planet's core, transforming the lush world into an imbalanced volcanic hellscape.",
                        Accessible = true,
                        Message = "Welcome to Mustafar, Once a garden world nourished by the Bright Star artifact, its orbit"+
                        " was shifted when Lady Corvax unleashed the energies of the Bright Star in an attempt to return her"+
                        " husband to life. The resulting gravimetric duel between the gas giants Jestefad and Lefrani over "+
                        "Mustafar heated the planet's core, transforming the lush world into an imbalanced volcanic hellscape.",
                        PlanetLog = "there was once a factory on this planet that was destroyed. You might be able to find the controls of a lightsaber in the ruins",
                        GameItems = new ObservableCollection<GameItemModel>
                            {
                                GameItemModelById(32),
                                GameItemModelById(21)
                            }
                        }
                );
            gamemap.Locations.Add
                (new Location()
                    {
                        ID = 5,
                        Name = "Dagobah",
                        PlanetColor = "/Lightsaber_Builder_Game;component/Graphics/Dagobah.jpg",
                        Description = "one of the purest places in the galaxy within the Force. A remote world of swamps and"+
                        " forests, it served as a refuge for Jedi Grand Master Yoda during his exile after the "+
                        "destruction of the Jedi Order.",
                        Accessible = true,
                        Message = "Welcome to Dagobah, Dagobah is a swampy planet said to have a strong connection with the force",
                        PlanetLog = "If you can get to where Master Yoda once Lived you might be able to find the Hand Grip to your Lightsaber",
                        GameItems = new ObservableCollection<GameItemModel>
                            {
                                GameItemModelById(33),
                                GameItemModelById(10)
                            }
                        }
                );
            gamemap.Locations.Add
                (new Location()
                    {
                        ID = 6,
                        Name = "Mandalore",
                        PlanetColor = "/Lightsaber_Builder_Game;component/Graphics/Mandalore.png",
                        Description = "It was the homeworld of the Mandalorians, a fearsome warrior people who fought the "+
                        "Jedi and raided their temple during the fall of the Old Republic. Years of war left the planet "+
                        "inhospitable, forcing the Mandalorians to live within domed cities. ",
                        Accessible = true,
                        Message = "Watch your back around here, the mandalorians are known as a peaceful race now but they"+
                        " used to be the most fearsome race in the galaxy",
                        PlanetLog = "You have gotten some intel that a Blade Emitter can be bought here",
                        GameItems = new ObservableCollection<GameItemModel>
                            {
                                GameItemModelById(30),
                                GameItemModelById(22),
                                GameItemModelById(20),
                                GameItemModelById(10)
                            }
                        }
                );
            gamemap.Locations.Add
                (new Location()
                    {
                        ID = 7,
                        Name = "Death Star",
                        PlanetColor = "/Lightsaber_Builder_Game;component/Graphics/DeathStar.png",
                        Description = "The Death Star was the Empire's ultimate weapon: a moon-sized space station with "+
                        "the ability to destroy an entire planet.",
                        Accessible = true,
                        Message = "This is the Death Star, here you can find the dark lords Darth Sidious and Darth Vader",
                        PlanetLog = "Once you have most of the lightsaber parts come back here to get your energy core",
                        GameItems = new ObservableCollection<GameItemModel>
                            {
                                GameItemModelById(34)
                            }
                        }
                );
            gamemap.Currentlocation = gamemap.Locations.FirstOrDefault(l => l.ID == 1);

            return gamemap;
        }
        private static GameItemModel GameItemModelById(int id)
        {
            return StandardGameItems().FirstOrDefault(i => i.Id == id);
        }
        public static List<GameItemModel> StandardGameItems() 
        {
            return new List<GameItemModel>()
            {
                new HealthItems(10, "Milk", 1, 15, 0, "Blue milk from the bantha cow"),
                new HealthItems(11, "Wookie Cookie", 3, 10, 0, "A flat, round piece of wookiefood that is also edible by other species"),
                new Credits(20, "Galactic Credit", 300, Credits.Currency.Credits, "Most commonly used Currency throughout the galaxy"),
                new Credits(21, "Coaxium", 200, Credits.Currency.Coaxium, "A Currency used by smugglers"),
                new Credits(22, "Mandalorian Credits", 100, Credits.Currency.MandalorianCredits, "Ancient currency once used only on Mandalor"),
                new LightSaberParts(30, "LIGHTSABER: Blade Emitter", 1, LightSaberParts.LightsaberPart.BladeEmitter, "Part of the lightsaber that emmits the lazer sword"),
                new LightSaberParts(31, "LIGHTSABER: Main Hilt", 1, LightSaberParts.LightsaberPart.MainHilt, "The upper part of the lightsaber that you hold on to"),
                new LightSaberParts(32, "LIGHTSABER: Controls", 1, LightSaberParts.LightsaberPart.Controls, "The controls to the lightsaber that allow you to control it"),
                new LightSaberParts(33, "LIGHTSABER: Hand Grip", 1, LightSaberParts.LightsaberPart.HandGrip, "The lower part of the lightsaber that you hold on to"),
                new LightSaberParts(34, "LIGHTSABER: Energy Core", 1, LightSaberParts.LightsaberPart.EnergyCore, "Part of a lightsaber that harnesses the energy and creates the lazer sword part"),
                new Weapons(40, "Blaster", 500, 0, 400, "A blaster where if used correctly, can be deadly"),
                new Weapons(41, "Lightsaber", 1000000, 100, 10000, "The lightsaber is a great weapon that can cut through most things in the universe")
            };
        }
    }
}
