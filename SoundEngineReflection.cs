using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;

namespace BetterGameUI {
    public static class SoundEngineReflection {
        public delegate void PlaySoundDelegate(
            int type, int x = -1, int y = -1, int Style = 1, float volumeScale = 1f, float pitchOffset = 0f);
        public static readonly PlaySoundDelegate PlaySound;

        static SoundEngineReflection() {
            PlaySound = typeof(SoundEngine).
                GetMethod("PlaySound", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(PlaySoundDelegate)) as PlaySoundDelegate;
        }
    }
}
