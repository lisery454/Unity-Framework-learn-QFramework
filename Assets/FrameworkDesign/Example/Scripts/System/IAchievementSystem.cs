using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace FrameworkDesign.Example {
    public interface IAchievementSystem : ISystem { }

    public class AchievementItem {
        public string Name { get; set; }
        public Func<bool> CheckComplete { get; set; }
        public bool Unlocked { get; set; }
    }

    public class AchievementSystem : AbstractSystem, IAchievementSystem {
        protected override void OnInit() {
            this.RegisterEvent<MissEvent>(e => { mMissed = true; });

            this.RegisterEvent<GameStartEvent>(e => { mMissed = false; });

            mItems.Add(new AchievementItem {
                Name = "百分!",
                CheckComplete = () => this.GetModel<IGameModel>().BestScore.Value > 100
            });

            mItems.Add(new AchievementItem {
                Name = "手残!",
                CheckComplete = () => this.GetModel<IGameModel>().Score.Value < 0
            });

            mItems.Add(new AchievementItem {
                Name = "零失误!",
                CheckComplete = () => !mMissed
            });

            mItems.Add(new AchievementItem {
                Name = "全成就!",
                CheckComplete = () => mItems.Count(item => item.Unlocked) >= 3
            });

            //成就如果要持久化也在这里做,让unlock;变成BindableProperty
            this.RegisterEvent<GamePassEvent>(async e => {
                await Task.Delay(TimeSpan.FromSeconds(0.1f));

                foreach (var item in mItems.Where(item => !item.Unlocked && item.CheckComplete())) {
                    item.Unlocked = true;
                    Debug.Log("解锁 成就 : " + item.Name);
                }
            });
        }

        private readonly List<AchievementItem> mItems = new List<AchievementItem>();
        private bool mMissed;
    }
}