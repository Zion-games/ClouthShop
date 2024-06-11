using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;


using CCL.GTAIV;

using IVSDKDotNet;
using IVSDKDotNet.Attributes;
using IVSDKDotNet.Enums;
using static System.Net.Mime.MediaTypeNames;
using static IVSDKDotNet.Native.Natives;
using System.Xml.Linq;
using System.Collections;
using ClouthShop;



namespace ClouthShop
{
    public class Main : Script
    {
        String text_script = "Hi wellcome to Suburban";
        int checkPointId = -1;
        IVPed playerPedNEW;
        public int playerPed;
        public int playerID;
        int open_menu_select = 0;

        bool ImGuiTestOpen = true;
        int selectedIndex = 0;

        string json;

        Vector3[] checkPoints = new Vector3[]
        {new Vector3(880.347f,-439.768f, 15.858f) // novinki cloths shop
        };

        enum ClouthType
        {Head, Torso,  Legs,  Suse,  Hands, Shoes,Hair,Sus2, Teeth, Face}


        List<ClothingItem> topList = new List<ClothingItem>();

        public Main()
        {
            Initialized += Main_Initialized;
            Uninitialize += Main_Uninitialize;
            Tick += Main_Tick;
            KeyDown += Main_KeyDown;
            Drawing += Main_Drawing;
            WaitTick += Main_WaitTick;
            WaitTickInterval = 3000;

        }

        private void Main_WaitTick(object sender, EventArgs e)
        {
            //IVGame.Console.Print("Tick from WaitTick event!");
        }

        private void Main_Initialized(object sender, EventArgs e)
        {
            playerPedNEW = IVPed.FromUIntPtr(IVPlayerInfo.FindThePlayerPed());
            playerID = CONVERT_INT_TO_PLAYERINDEX(GET_PLAYER_ID());

            try
            {
                json = File.ReadAllText("IVSDKDotNet/scripts/clouth_data.json");//READ THE JSON FILE
                IVGame.Console.Print("json: " + json);
                ClothingData data = JsonSerializer.Deserialize<ClothingData>(json);
                //topList = data.Top;
            }
            catch (Exception E)
            {
                IVGame.Console.PrintError("Error: " + E.Message);
            }

            //topList.Add(new ClothingItem { Name = "T-Shirt", index = 0, Desc = "A simple T-Shirt", Price = 100, Owned = true });
            topList.Add(new ClothingItem { Name = "Varsity Jacket", index = 0, texture = 0, Desc = "black and white Varsity Jacket.\nmade by R*Star", Price = 100, Owned = true });
            //topList.Add(new ClothingItem { Name = "T-Shirt", index = 2, texture = 1, Desc = "A simple T-Shirt\n made by R*Star", Price = 100, Owned = true });
            //topList.Add(new ClothingItem { Name = "Suit Jaket and button shirt", index = 2, texture = 0, Desc = "A simple T-Shirt\n made by R*Star", Price = 100, Owned = false });
            //topList.Add(new ClothingItem { Name = "Suit Jaket and button shirt", index = 2,  Desc = "A simple T-Shirt\n made by R*Star", Price = 100, Owned = false, texture = 1, });



            GET_PLAYER_CHAR(playerID, out playerPed);

            


        
           

            IVGame.Console.RegisterCommand(this, "tel",
                (string[] args) =>
                {
                    if (args.Length < 4)
                    {
                        IVGame.Console.PrintWarning("add argument 4 x, y, z t/f");
                    }
                    else
                    {
                        float x = float.Parse(args[0]);
                        float y = float.Parse(args[1]);
                        float z = float.Parse(args[2]);
                        Vector3 newPosition = new Vector3(x, y, z); // create a new Vector3 with the desired coordinates


                        playerPedNEW.Teleport(newPosition, false, false); // teleport the player to the new position
                    }
                    IVGame.Console.PrintWarning("tel called: " + args.Length);
                });

            IVGame.Console.RegisterCommand(this, "clouth",
                (string[] args) =>
                {

             
                    if (args.Length < 3)
                    {
                        IVGame.Console.PrintWarning("add argument drawable, texture, palette");
                    }
                    else
                    {
                        uint component = uint.Parse(args[0]);
                        uint model = uint.Parse(args[1]);
                        uint textuer = uint.Parse(args[2]);
                        SET_CHAR_COMPONENT_VARIATION(playerPed, component, model, textuer); // Set the drawable of the player
                        IVGame.Console.PrintWarning("the player " + playerPed + " \nset component:" + component + " model: " + model + " textuer: " + textuer);
                    }

                });
            IVGame.Console.RegisterCommand(this, "sel",
                (string[] args) =>
                {
                    if (args.Length < 1)
                    {
                        IVGame.Console.PrintWarning("add argument 1");
                        return;
                    }
                    open_menu_select = int.Parse(args[0]);
                    IVGame.Console.PrintWarning("sel called: " + open_menu_select);


                });


        }
        private void Main_Uninitialize(object sender, EventArgs e)
        {
            IVGame.Console.Print("Uninitialize");
        }

        private void Main_Tick(object sender, EventArgs e)
        {

        }


        private void Main_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.U)
            {
                if (open_menu_select == 2)
                {
                    open_menu_select = 0;
                }
                else
                {
                    open_menu_select = 2;
                }
            }
            if (e.KeyCode == Keys.I)
            {
            }

        }

        [Obsolete]
        private void Main_Drawing(object sender, EventArgs e)
        {
            // Flags.TryGetValue("button pressed", out bool buttonPressed);

            ImGuiIV.AddDrawCommand(this, () =>
            {
                if (open_menu_select == 1)
                {
                    if (ImGuiIV.BeginCanvas(this, out ImGuiIV_DrawingContext ctx))
                    {
                        //IVGame.Console.Print("begin canvas");
                        //ctx.AddRectFilledMultiColor(new Vector2(100f, 100f), new Vector2(200f, 200f), Color.Red, Color.Green, Color.Blue, Color.Yellow);

                        //ctx.AddText(new Vector2(500f, 200f), Color.Brown, 50f, "yo yo!");
                        ctx.AddBezierCubic(new Vector2(100f, 100f), new Vector2(200f, 200f), new Vector2(300f, 100f), new Vector2(400f, 200f), Color.Red, 5f, 3);

                        ImGuiIV.EndCanvas();

                    }
                }
                if (open_menu_select == 2)
                {
                    Clouth_menu();
                }
                else
                {
                    return;
                }

            });
        }

        private void Clouth_menu()
        {

            ImGuiIV.Begin("clouths menu",ref ImGuiTestOpen, eImGuiWindowFlags.NoTitleBar, false);
            
            if(ImGuiIV.CloseButton(3, new Vector2(ImGuiIV.GetWindowWidth() - 10, 50f)))
            {
                open_menu_select = 0;
            }

            if (ImGuiIV.BeginListBox("\nclouths top\n", new Vector2(ImGuiIV.GetWindowWidth()/2, 300f)))
            {
                for (int i = 0; i < topList.Count; i++)
                {
                    if (ImGuiIV.Selectable(topList[i].Name))
                    {
                        selectedIndex = i;
                        SET_CHAR_COMPONENT_VARIATION(playerPed, ((uint)ClouthType.Torso) , topList[i].index, topList[i].texture); // Set the drawable of the player
                    }
                }
                
                ImGuiIV.EndListBox();
            }
            ImGuiIV.NewLine();
            ImGuiIV.Text("topList[i].index" + topList[selectedIndex].index);
            ImGuiIV.NewLine();
            ImGuiIV.Text("topList[i].texture " + topList[selectedIndex].texture);


            ImGuiIV.NewLine();
            ImGuiIV.Text("" + topList[selectedIndex].Desc);
            ImGuiIV.NewLine();
            ImGuiIV.Text("Price: " + topList[selectedIndex].Price);

            if (topList[selectedIndex].Owned)
            {
                //ImGuiIV.Text("Owned");
                ImGuiIV.Button("Owned");
          
            }
            else
            {
                if (ImGuiIV.Button("Buy"))
                {
                    IVGame.Console.Print("Buy");
                }
            }



            ImGuiIV.End();
        }
        
        private int items_list()
        {
            ImGuiIV.Begin("Items List", ref ImGuiTestOpen, eImGuiWindowFlags.NoTitleBar, false);
            ImGuiIV.Text("Items List");
            ImGuiIV.End();
            return 0;
        }

    }
}
