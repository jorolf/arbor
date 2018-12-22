﻿using System.Linq;
using eden.Game.IO;
using eden.Game.Worlds;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;

namespace eden.Game.Screens.MapEditor.MapWindows
{
    public class OpenWorldWindow : MapWindow<World>
    {
        private Dropdown<string> worldDropdown;
        private Story story;
        private JsonStore jsonStore;

        [BackgroundDependencyLoader]
        private void load(Story story, JsonStore jsonStore)
        {
            this.story = story;
            this.jsonStore = jsonStore;
            Title = "Worlds";
            SubmitText = "Open World";

            Add(new NamedContainer("World: ", worldDropdown = new BasicDropdown<string>
            {
                RelativeSizeAxes = Axes.X,
                Items = story.WorldFiles.ToDictionary(s => s)
            })
            {
                RelativeSizeAxes = Axes.None,
                Width = 400
            });
        }

        public override void Show()
        {
            base.Show();

            worldDropdown.Items = story.WorldFiles.ToDictionary(s => s);
        }

        protected override World SubmitParam
        {
            get
            {
                World world = jsonStore.Deserialize<World>(worldDropdown.Current);
                world.WorldName = worldDropdown.Current;
                return world;
            }
        }

        protected override bool ValuesValid() => true;

        protected override void ResetValues()
        { }
    }
}