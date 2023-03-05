using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace TriColor
{
    internal class AudioManager
    {
        private static bool audioFocusAcquired;
        private static AudioEngine audioEngine;
        private static WaveBank waveBank;
        private static SoundBank soundBank;

        public static void Initialize()
        {
            audioEngine = new AudioEngine("Content/game_audio.xgs");
            waveBank = new WaveBank(audioEngine, "Content/game_wavebank.xwb");
            soundBank = new SoundBank(audioEngine, "Content/game_soundbank.xsb");
        }

        public static void RequestAudioFocus()
        {
            if (!audioFocusAcquired)
            {
                //audioEngine.Activate();
                audioFocusAcquired = true;
            }
        }

        public static void ReleaseAudioFocus()
        {
            if (audioFocusAcquired)
            {
                //audioEngine.Deactivate();
                audioFocusAcquired = false;
            }
        }

        public static void PlaySound(string soundName)
        {
            if (audioFocusAcquired)
            {
                //SoundEffect soundEffect = soundBank.GetCue(soundName).WaveBankData.SoundData.Format == WaveFormatTag.WMA ? null : new SoundEffect(soundBank.GetCue(soundName).WaveBankData.SoundData, soundBank.GetCue(soundName).WaveBankData.Sounds[0].Offset, soundBank.GetCue(soundName).WaveBankData.Sounds[0].Size);
                //soundEffect?.Play();
            }
        }
    }
}
