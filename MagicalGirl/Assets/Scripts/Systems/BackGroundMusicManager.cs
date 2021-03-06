﻿using System;
using UnityEngine;
using UniRx;

namespace Systems {
    /// <summary>
    /// BGM関連のシングルトンクラス
    /// シーンに応じてBGMを切り替えるようにしている
    /// </summary>
    public class BackGroundMusicManager : MonoBehaviour {
        private static BackGroundMusicManager instance;
        public static BackGroundMusicManager Instance => instance;
        [SerializeField] private AudioClip[] audioClips;
        private AudioSource audioSource;

        void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            StartMusic(SceneManager.NowScene);
        }

        public void StartMusic(Scene nowScene) {
            audioSource.Stop();
            switch (nowScene) {
                case Scene.Title:
                    break;
                case Scene.Game:
                    audioSource.clip = audioClips[0];
                    audioSource.Play();
                    break;
                case Scene.Boss:
                    audioSource.clip = audioClips[1];
                    audioSource.Play();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("nowScene", nowScene, null);
            }
        }
    }
}
