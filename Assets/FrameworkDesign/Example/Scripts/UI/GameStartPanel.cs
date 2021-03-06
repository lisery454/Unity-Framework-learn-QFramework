using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example {
    public class GameStartPanel : MonoBehaviour, IController {
        private IGameModel mGameModel;

        private void Start() {
            transform.Find("BtnStart").GetComponent<Button>()
                .onClick.AddListener(() => {
                    gameObject.SetActive(false);
                    this.SendCommand<StartGameCommand>();
                });

            transform.Find("BtnBuyLife").GetComponent<Button>()
                .onClick.AddListener(() => { this.SendCommand<BuyLifeCommand>(); });

            mGameModel = this.GetModel<IGameModel>();

            mGameModel.Gold.RegisterOnValueChanged(OnGoldValueChanged);
            mGameModel.Life.RegisterOnValueChanged(OnLifeValueChanged);

            //第一次初始化调用
            OnGoldValueChanged(mGameModel.Gold.Value);
            OnLifeValueChanged(mGameModel.Life.Value);
        }

        private void OnLifeValueChanged(int life) {
            transform.Find("LifeText").GetComponent<Text>().text = "生命:" + life;
        }

        private void OnGoldValueChanged(int gold) {
            transform.Find("BtnBuyLife").gameObject.SetActive(gold > 0);

            transform.Find("GoldText").GetComponent<Text>().text = "金币:" + gold;
        }

        private void OnDestroy() {
            mGameModel.Gold.UnregisterOnValueChanged(OnGoldValueChanged);
            mGameModel.Life.UnregisterOnValueChanged(OnLifeValueChanged);
            mGameModel = null;
        }

        IArchitecture IBelongToArchitecture.GetArchitecture() {
            return PointGame.Interface;
        }
    }
}