using Controller.Characters;
using Model;
using Model.Building;
using System.Collections.Generic;
using UnityEngine;
using View;
using View.Game;
using View.UI;

namespace Controller.Building
{
    public class RackController : UseablePlaceableObjectController
    {
        private RackViewPanel _viewPanel;
        private RackModel _rackModel;
        private WorkController _workController;

        public List<string> GetTargetItems => _rackModel.TargetItems;

        public override void Construct()
        {
            _viewPanel = GameContext.DIContainer.Resolve<UIManager>().GetPanel<RackViewPanel>();
            _workController = GameContext.DIContainer.Resolve<WorkController>();
            _viewPanel.OnTaskCreated += CreateTask;
            _inventoryController = new ItemCollectionController(_objectModel.ItemCollectionModel);
        }

        public override PlaceableObjectController SetModel(PlaceableObjectModel placeableObject)
        {
            if (string.IsNullOrEmpty(placeableObject.Data))
            {
                _rackModel = new RackModel(placeableObject.Id, placeableObject.ItemCollectionModel.Size);
            }
            else
            {
                _rackModel = JsonUtility.FromJson<RackModel>(placeableObject.Data);
            }
            return base.SetModel(placeableObject);
        }

        public override void SaveData()
        {
            _objectModel.SetData(JsonUtility.ToJson(_rackModel));
        }

        public override void OpenUI()
        {
            _viewPanel.Open(_rackModel, _inventoryController.ItemCollectionModel);
        }

        private void CreateTask()
        {
            _rackModel.SetTargetItems(_viewPanel.TargetItems);
            _workController.AddTask(_objectModel.Id);
        }

        public override void Use(NPCConroller commandConroller)
        {
            throw new System.NotImplementedException();
        }
    }
}