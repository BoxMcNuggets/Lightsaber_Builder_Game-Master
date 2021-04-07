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
        public static Player.ForceSide PlayerJobTitle()
        {
            if (true)
            {
                return Player.ForceSide.DarkSide;
            }
            else
            {
                return Player.ForceSide.LightSide;
            }
        }

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
                Inventory = new ObservableCollection<GameItemModelQuantity>()
                {

                }
            };
        }
        #region Create GameMap

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
                GameItems = new ObservableCollection<GameItemModelQuantity>
                {
                    new GameItemModelQuantity(GameItemModelById(40), 1),
                    new GameItemModelQuantity(GameItemModelById(10), 1),
                    new GameItemModelQuantity(GameItemModelById(11), 1)
                }
            }
            );

            gamemap.Currentlocation = gamemap.Locations.FirstOrDefault(l => l.ID == 1);

            switch (PlayerJobTitle())
            {
                case Player.ForceSide.LightSide:
                    gamemap.Locations.Add
                        (new Location()
                        {
                            ID = 3,
                            Name = "Coruscant",
                            PlanetColor = "/Lightsaber_Builder_Game;component/Graphics/Coruscant.jpg",
                            Description = "Coruscant is a city-covered planet with a population of one trillion beings." +
                                " Coruscant's surface was defined by its urban sprawl, which collectively was called Galactic City",
                            Accessible = true,
                            Message = "The city covered planet, It might be usefull to do some shopping while your here",
                            PlanetLog = "Once you have most of the lightsaber parts come back here to get your energy core",
                            GameItems = new ObservableCollection<GameItemModelQuantity>
                            {
                                new GameItemModelQuantity(GameItemModelById(34), 1),
                                new GameItemModelQuantity(GameItemModelById(20), 1)
                            },
                            NPCs = new ObservableCollection<NPCS>
                            {
                                NPCById(102)
                            }
                        }
                        );
                    break;
                case Player.ForceSide.DarkSide:
                    gamemap.Locations.Add
                        (new Location()
                        {
                            ID = 2,
                            Name = "Death Star",
                            PlanetColor = "/Lightsaber_Builder_Game;component/Graphics/DeathStar.png",
                            Description = "The Death Star was the Empire's ultimate weapon: a moon-sized space station with " +
                                    "the ability to destroy an entire planet.",
                            Accessible = true,
                            Message = "This is the Death Star, here you can find the dark lords Darth Sidious and Darth Vader",
                            PlanetLog = "Once you have most of the lightsaber parts come back here to get your energy core",
                            GameItems = new ObservableCollection<GameItemModelQuantity>
                            {
                                new GameItemModelQuantity(GameItemModelById(34), 1)
                            },
                            NPCs = new ObservableCollection<NPCS>
                            {
                                NPCById(107),
                                NPCById(101)
                            }
                        }
                        );
                    break;
                default:
                    break;
            }
            gamemap.Locations.Add
                (new Location()
                {
                    ID = 4,
                    Name = "Hoth",
                    PlanetColor = "/Lightsaber_Builder_Game;component/Graphics/Hoth.png",
                    Description = "Hoth is the sixth planet in the remote system of the same name, and was the site of the" +
                        " Rebel Alliance's Echo Base. It is a world of snow and ice, surrounded by numerous moons, and home to" +
                        " deadly creatures like the wampa.",
                    Accessible = true,
                    Message = "Hoth is a frozen winter planet with lots of life, but be careful because not everything is friendly",
                    PlanetLog = "Wampas are notorious on the planet hoth, im sure you could find some lightsaber parts in one of their caves",
                    GameItems = new ObservableCollection<GameItemModelQuantity>
                    {
                        new GameItemModelQuantity(GameItemModelById(31), 1),
                        new GameItemModelQuantity(GameItemModelById(20), 1),
                        new GameItemModelQuantity(GameItemModelById(10), 1),
                        new GameItemModelQuantity(GameItemModelById(11), 1),
                        new GameItemModelQuantity(GameItemModelById(50), 1)
                    },
                    NPCs = new ObservableCollection<NPCS>
                    {
                        NPCById(105)
                    }
                }
                );
            gamemap.Locations.Add
                (new Location()
                {
                    ID = 5,
                    Name = "Mustafar",
                    PlanetColor = "/Lightsaber_Builder_Game;component/Graphics/mustafar.jpg",
                    Description = "Once a garden world nourished by the Bright Star artifact, its orbit was shifted when " +
                        "Lady Corvax unleashed the energies of the Bright Star in an attempt to return her husband to life. " +
                        "The resulting gravimetric duel between the gas giants Jestefad and Lefrani over Mustafar heated the " +
                        "planet's core, transforming the lush world into an imbalanced volcanic hellscape.",
                    Accessible = true,
                    Message = "Welcome to Mustafar, Once a garden world nourished by the Bright Star artifact, its orbit" +
                        " was shifted when Lady Corvax unleashed the energies of the Bright Star in an attempt to return her" +
                        " husband to life. The resulting gravimetric duel between the gas giants Jestefad and Lefrani over " +
                        "Mustafar heated the planet's core, transforming the lush world into an imbalanced volcanic hellscape.",
                    PlanetLog = "there was once a factory on this planet that was destroyed. You might be able to find the controls of a lightsaber in the ruins",
                    GameItems = new ObservableCollection<GameItemModelQuantity>
                    {
                        new GameItemModelQuantity(GameItemModelById(32), 1),
                        new GameItemModelQuantity(GameItemModelById(21), 1),
                        new GameItemModelQuantity(GameItemModelById(52), 1)
                    },
                    NPCs = new ObservableCollection<NPCS>
                    {
                        NPCById(108)
                    }
                }
                );
            gamemap.Locations.Add
                (new Location()
                {
                    ID = 6,
                    Name = "Dagobah",
                    PlanetColor = "/Lightsaber_Builder_Game;component/Graphics/Dagobah.jpg",
                    Description = "one of the purest places in the galaxy within the Force. A remote world of swamps and" +
                        " forests, it served as a refuge for Jedi Grand Master Yoda during his exile after the " +
                        "destruction of the Jedi Order.",
                    Accessible = true,
                    Message = "Welcome to Dagobah, Dagobah is a swampy planet said to have a strong connection with the force",
                    PlanetLog = "If you can get to where Master Yoda once Lived you might be able to find the Hand Grip to your Lightsaber",
                    GameItems = new ObservableCollection<GameItemModelQuantity>
                    {
                        new GameItemModelQuantity(GameItemModelById(33), 1),
                        new GameItemModelQuantity(GameItemModelById(10), 1),
                        new GameItemModelQuantity(GameItemModelById(51), 1)
                    },
                    NPCs = new ObservableCollection<NPCS>
                    {
                        NPCById(104)
                    }
                }
                );
            gamemap.Locations.Add
                (new Location()
                {
                    ID = 7,
                    Name = "Mandalore",
                    PlanetColor = "/Lightsaber_Builder_Game;component/Graphics/Mandalore.png",
                    Description = "It was the homeworld of the Mandalorians, a fearsome warrior people who fought the " +
                        "Jedi and raided their temple during the fall of the Old Republic. Years of war left the planet " +
                        "inhospitable, forcing the Mandalorians to live within domed cities. ",
                    Accessible = true,
                    Message = "Watch your back around here, the mandalorians are known as a peaceful race now but they" +
                        " used to be the most fearsome race in the galaxy",
                    PlanetLog = "You have gotten some intel that a Blade Emitter can be bought here",
                    GameItems = new ObservableCollection<GameItemModelQuantity>
                    {
                        new GameItemModelQuantity(GameItemModelById(30), 1),
                        new GameItemModelQuantity(GameItemModelById(22), 1),
                        new GameItemModelQuantity(GameItemModelById(20), 1),
                        new GameItemModelQuantity(GameItemModelById(10), 1),
                        new GameItemModelQuantity(GameItemModelById(53), 1)
                    },
                    NPCs = new ObservableCollection<NPCS>
                    {
                        NPCById(103)
                    }
                }
                );

            gamemap.Currentlocation = gamemap.Locations.FirstOrDefault(l => l.ID == 1);

            return gamemap;
        }

        #endregion

        #region Create Items

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
                new LightSaberParts(30, "LIGHTSABER: Blade Emitter", 1, LightSaberParts.LightsaberPart.BladeEmitter, "Part of the lightsaber that emmits the lazer sword", 17),
                new LightSaberParts(31, "LIGHTSABER: Main Hilt", 1, LightSaberParts.LightsaberPart.MainHilt, "The upper part of the lightsaber that you hold on to", 17),
                new LightSaberParts(32, "LIGHTSABER: Controls", 1, LightSaberParts.LightsaberPart.Controls, "The controls to the lightsaber that allow you to control it", 17),
                new LightSaberParts(33, "LIGHTSABER: Hand Grip", 1, LightSaberParts.LightsaberPart.HandGrip, "The lower part of the lightsaber that you hold on to", 17),
                new LightSaberParts(34, "LIGHTSABER: Energy Core", 1, LightSaberParts.LightsaberPart.EnergyCore, "Part of a lightsaber that harnesses the energy and creates the lazer sword part", 17),
                new Weapons(40, "Blaster", 500, 100, 400, "A blaster where if used correctly, can be deadly", "Blaster"),
                new Weapons(41, "Lightsaber", 1000000, 100, 10000, "The lightsaber is a great weapon that can cut through most things in the universe", "Lightsaber"),
                new KyberCrystals(50, "Blue Kyber Crystal", 1, KyberCrystals.KyberCrystal.Blue, "The part of the lightsaber which gives it color", 15),
                new KyberCrystals(51, "Green Kyber Crystal", 1, KyberCrystals.KyberCrystal.Green, "The part of the lightsaber which gives it color", 15),
                new KyberCrystals(52, "Red Kyber Crystal", 1, KyberCrystals.KyberCrystal.Red, "The part of the lightsaber which gives it color", 15),
                new KyberCrystals(53, "Black Kyber Crystal", 1, KyberCrystals.KyberCrystal.Black, "The part of the lightsaber which gives it color", 15)
            };
        }

        #endregion

        #region Create NPCS

        private static NPCS NPCById(int id) 
        {
            return NPCs().FirstOrDefault(i => i.Id == id);
        }
        public static List<NPCS> NPCs()
        {
            return new List<NPCS>()
            {
                new NPCSpeak()
                {
                    Id = 101,
                    Name = "Darth Vader",
                    Race = Character.RaceType.Human, 
                    Description = "Once a jedi, Darth Vader is now just a black mechanical suit which strikes fear throughout the universe.", 
                    Messages = new List<string>()
                    {
                        "I am altering the deal. Pray I don’t alter it any further.",
                        "Be careful not to choke on your aspirations."
                    }
                },
                new NPCSpeak()
                {
                    Id = 102,
                    Name = "Obi-Wan Kenobi",
                    Race = Character.RaceType.Human,
                    Description = "A legendary Jedi Master, Obi-Wan Kenobi was a noble man and gifted in the ways of the Force.",
                    Messages = new List<string>()
                    {
                        "Hello there.",
                        "You’re going to find that many of the truths we cling to depend greatly on our own point of view. The truth is often what we make of it."
                    }
                },
                new NPCSpeak()
                {
                    Id = 103,
                    Name = "Duchess Satine Kryze",
                    Race = Character.RaceType.Human,
                    Description = "The leader of Mandalore during the Clone Wars, Duchess Satine of Kalevala was a controversial figure. She longed to move Mandalore beyond its violent past and instituted a government that valued pacifism.",
                    Messages = new List<string>()
                    {
                        "I remember a time when Jedi were not generals, but peacekeepers.",
                        "I speak for thousands of worlds that have urged me to allow them to stay neutral in this war."
                    }
                },
                new NPCSpeak()
                {
                    Id = 104,
                    Name = "Yoda",
                    Race = Character.RaceType.Unknown,
                    Description = "Yoda was a legendary Jedi Master and stronger than most in his connection with the Force. Small in size but wise and powerful.",
                    Messages = new List<string>()
                    {
                        "Do or do not. There is no try.",
                        "The greatest teacher, failure is."
                    }
                },
                new NPCSpeak()
                {
                    Id = 105,
                    Name = "Princess Leia",
                    Race = Character.RaceType.Human,
                    Description = "Princess Leia Organa was one of the Rebel Alliance's greatest leaders, fearless on the battlefield and dedicated to ending the tyranny of the Empire",
                    Messages = new List<string>()
                    {
                        "Aren't You A Little Short For A Stormtrooper?",
                        "You Came In That Thing? You're Braver Than I Thought!"
                    }
                },
                new NPCSpeak()
                {
                    Id = 106,
                    Name = "Droid",
                    Race = Character.RaceType.Droid,
                    Description = "A common R2 unit used by most people in the universe.",
                    Messages = new List<string>()
                    {
                        "The droid makes beeping noises as you look at it.",
                        "The droid makes sad beeping noises as you make a gesture towards it."
                    }
                },
                new NPCSpeak()
                {
                    Id = 107,
                    Name = "Emperor Palpatine",
                    Race = Character.RaceType.Human,
                    Description = "Scheming, powerful, and evil to the core, Darth Sidious restored the Sith and destroyed the Jedi Order",
                    Messages = new List<string>()
                    {
                        "Your Feeble Skills Are No Match For The Power Of The Dark Side.",
                        "Everything That Has Transpired Has Done So According To My Design."
                    }
                },
                new NPCSpeak()
                {
                    Id = 108,
                    Name = "Wilhuff Tarken",
                    Race = Character.RaceType.Human,
                    Description = "An ambitious, ruthless proponent of military power, Wilhuff Tarkin became a favorite of Supreme Chancellor Palpatine and rose rapidly through the Imperial ranks.",
                    Messages = new List<string>()
                    {
                        "Welcome to Mustafar",
                        "The Jedi are extinct. Their fire has gone out of the universe. You, my friend, are all that's left of their religion."
                    }
                }
            };
        }

        #endregion

    }
}
