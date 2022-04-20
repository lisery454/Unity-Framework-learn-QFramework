﻿using System;

namespace FrameworkDesign.Example {
    public interface ICountDownSystem : ISystem {
        int CurrentRemainSeconds { get; }
        void Update();
    }

    public class CountDownSystem : AbstractSystem, ICountDownSystem {
        protected override void OnInit() {
            this.RegisterEvent<GameStartEvent>(e => {
                mStarted = true;
                mGameStartTime = DateTime.Now;
            });

            this.RegisterEvent<GamePassEvent>(e => { mStarted = false; });
        }

        public int CurrentRemainSeconds => 10 - (int) (DateTime.Now - mGameStartTime).TotalSeconds;

        private DateTime mGameStartTime { get; set; }
        private bool mStarted;


        public void Update() {
            if (mStarted) {
                if (DateTime.Now - mGameStartTime > TimeSpan.FromSeconds(10)) {
                    this.SendEvent<CountDownEndEvent>();
                    mStarted = false;
                }
            }
        }
    }
}