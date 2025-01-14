using System.Collections.Generic;
using UI.Raid.Creation.Unit;
using Unit.Config;
using Unit.Creation;
using Unit.Raid;
using UnityEngine;

namespace UI.Raid.Creation
{
	public class RaidCreationPanel : UIModel<RaidCreationPanelView>
	{
		public List<UnitConfig> Archetypes
		{
			set => _view.Archetypes = value;
		}
		
		public RaidTemplate RaidTemplate
		{
			get => _view.Template; 
			set => _view.Template = value;
		}

		public Vector2Int Position
		{
			get;
			set;
		}
		
		private UnitCreationPanel _unitPanel;

		private RaidManager _raidManager;
		
		public RaidCreationPanel(RaidCreationPanelView view, UnitCreationPanel unitCreationPanel, RaidManager raidManager) : base(view)
		{
			_view.OnArchetypeSelect += ArchetypeSelect;
			_view.OnApply += Apply;
			_view.OnClose += Close;
			
			_unitPanel = unitCreationPanel;
			_unitPanel.OnCreationApplied += CreationApplied;

			_raidManager = raidManager;
		}

		private void CreationApplied(UnitTemplate unitTemplate)
		{
			Show();
			RaidTemplate.TryAddUnit(unitTemplate);
		}

		private void ArchetypeSelect(UnitConfig archetype)
		{
			Hide();
			_unitPanel.Template = new UnitTemplate(archetype);
			_unitPanel.Show();
		}

		private void Close()
		{
			Hide();
		}

		private void Apply()
		{
			_raidManager.CreateRaid(RaidTemplate, Position);
			Hide();
		}
	}
}