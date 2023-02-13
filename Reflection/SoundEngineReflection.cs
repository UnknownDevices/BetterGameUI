using System.Reflection;
using Terraria.Audio;

namespace BetterGameUI.Reflection
{
    public static class SoundEngineReflection
    {
        public static readonly MethodInfo PlaySoundInfo;

        public static void PlaySound(int type, int x = -1, int y = -1, int Style = 1,
            float volumeScale = 1f, float pitchOffset = 0f) =>
            PlaySoundInfo.Invoke(null, new object[] { type, x, y, Style, volumeScale, pitchOffset });

        static SoundEngineReflection()
        {
            PlaySoundInfo = typeof(Terraria.Audio.SoundEngine).
            GetMethod("PlaySound",
            BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly,
            new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(float), typeof(float) });
        }
    }
}