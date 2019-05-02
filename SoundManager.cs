using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPSoundBoard.Model
{
    public class SoundManager
    {
        public static void GetAllSounds(ObservableCollection<Sound> sounds)
        {
            var allSounds = getSounds();    // get the List of sounds
            sounds.Clear();                 
            allSounds.ForEach(p => sounds.Add(p));  // LINQ to get all sounds
        }

        public static void GetSoundsByCategory(ObservableCollection<Sound> sounds, SoundCategory soundCategory)
        {
            var allsounds = getSounds();
            var filteredSounds = allsounds
                .Where(p => p.Category == soundCategory)
                .ToList();
            sounds.Clear();
            filteredSounds.ForEach(p => sounds.Add(p));
        }

        public static void GetSoundsByName(ObservableCollection<Sound> sounds, string name)
        {
            var allSounds = getSounds();
            var filteredSounds = allSounds
                .Where(p => p.Name == name)
                .ToList();
            sounds.Clear();
            filteredSounds.ForEach(p => sounds.Add(p));
        }

        private static List<Sound> getSounds()
        {
            var sounds = new List<Sound>();

            sounds.Add(new Sound("Cow", SoundCategory.Animals));
            sounds.Add(new Sound("Cat", SoundCategory.Animals));
            sounds.Add(new Sound("Dog", SoundCategory.Animals));
            sounds.Add(new Sound("Eagle", SoundCategory.Animals));
            sounds.Add(new Sound("Goat", SoundCategory.Animals));


            sounds.Add(new Sound("Gun", SoundCategory.Cartoons));
            sounds.Add(new Sound("Jack", SoundCategory.Cartoons));
            sounds.Add(new Sound("Run", SoundCategory.Cartoons));
            sounds.Add(new Sound("Slip", SoundCategory.Cartoons));
            sounds.Add(new Sound("Spring", SoundCategory.Cartoons));

            sounds.Add(new Sound("GabeN", SoundCategory.Dota2));
            sounds.Add(new Sound("Invoker", SoundCategory.Dota2));
            sounds.Add(new Sound("InvokerInvoke", SoundCategory.Dota2));
            sounds.Add(new Sound("InvokerRaM", SoundCategory.Dota2));
            sounds.Add(new Sound("Meepo", SoundCategory.Dota2));
            sounds.Add(new Sound("MeepoPoof", SoundCategory.Dota2));
            sounds.Add(new Sound("MeepoRaM", SoundCategory.Dota2));
            sounds.Add(new Sound("Pudge", SoundCategory.Dota2));
            sounds.Add(new Sound("PudgeHook", SoundCategory.Dota2));
            sounds.Add(new Sound("PudgeRaM", SoundCategory.Dota2));
            sounds.Add(new Sound("ShadowFiend", SoundCategory.Dota2));
            sounds.Add(new Sound("ShadowFiendRequiem", SoundCategory.Dota2));
            sounds.Add(new Sound("ShadowFiendRaM", SoundCategory.Dota2));
            sounds.Add(new Sound("Techies", SoundCategory.Dota2));
            sounds.Add(new Sound("TechiesRemoteMine", SoundCategory.Dota2));
            sounds.Add(new Sound("TechiesRaM", SoundCategory.Dota2));
            sounds.Add(new Sound("Tinker", SoundCategory.Dota2));
            sounds.Add(new Sound("TinkerLaser", SoundCategory.Dota2));
            sounds.Add(new Sound("TinkerRaM", SoundCategory.Dota2));

            sounds.Add(new Sound("CJ", SoundCategory.Memes));
            sounds.Add(new Sound("Profanity", SoundCategory.Memes));
            sounds.Add(new Sound("Illuminati", SoundCategory.Memes));
            sounds.Add(new Sound("JustDoIt", SoundCategory.Memes));

            sounds.Add(new Sound("Chucky", SoundCategory.Taunts));
            sounds.Add(new Sound("Clock", SoundCategory.Taunts));
            sounds.Add(new Sound("Horn", SoundCategory.Taunts));
            sounds.Add(new Sound("LOL", SoundCategory.Taunts));
            sounds.Add(new Sound("Peter", SoundCategory.Taunts));
            sounds.Add(new Sound("YAY", SoundCategory.Taunts));

            sounds.Add(new Sound("Alert", SoundCategory.Warnings));
            sounds.Add(new Sound("Ship", SoundCategory.Warnings));
            sounds.Add(new Sound("Siren", SoundCategory.Warnings));

            return sounds;
        }
    } // end class
} // end namespace
