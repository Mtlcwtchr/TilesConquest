using World;

namespace UI.Storage
{
	public class StoragePanel
	{
		private StoragePanelView _view;
		private World.Storage _storage;
		
		public StoragePanel(StoragePanelView view, World.Storage storage)
		{
			_view = view;
			_storage = storage;
			
			_storage.OnResourceAdvanced += ResourceAdvanced;
			ResourceAdvanced(EResource.Gold, _storage.Get(EResource.Gold));
			ResourceAdvanced(EResource.Goods, _storage.Get(EResource.Goods));
			ResourceAdvanced(EResource.Food, _storage.Get(EResource.Food));
		}

		private void ResourceAdvanced(EResource resource, int amount)
		{
			_view.SetResource(resource, amount);
		}
	}
}