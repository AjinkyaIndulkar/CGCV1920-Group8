using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum VH : int
{ 
V=0,
H=1
}

public enum DIR : int
{ 
X = 0,
N,
S,
E,
W
}

public enum CORNER : int
{
    NW = 1,
    SW,
    SE,
    NE
}



public class MazeGen : MonoBehaviour
{
    // Start is called before the first frame update
    
    System.Random GBR = new System.Random(0);

    MazeClass.MazeGenerator mc;

    public int size_x;
    public int size_y;

    public Dropdown dropdown;
    public Button button;
    public Slider slider;

    public Camera cam_info;

    private float vw_max_x = 10.0f;
    private float vw_max_y = 10.0f;

    void create_random_walls(int n=100)
    {
        while (n-->0)
            create_random_wall();
    }

    void create_random_wall()
    {
        var rndpos = new Vector3((float)GBR.NextDouble()*10, (float)GBR.NextDouble()*10, 0);

        create_wall(rndpos,1.0f,GBR.Next(0,10)%2);


    }
    /*
    void draw_all_walls()
    {
        for (int i = 0; i < size_x; i++)
        {
            for (int j = 0; j < size_y; j++)
            {
                walls_h[i, j + 1] = 1;
                walls_h[i, j + 1] = 1;
                switch (walking_dir[i, j])
                {

                    case DIR.N:
                        walls_h[i, j + 1] = 0;
                        break;
                    case DIR.S:
                        walls_h[i, j] = 0;
                        break;
                    case DIR.E:
                        walls_v[i + 1, j] = 0;
                        break;
                    case DIR.W:
                        walls_v[i, j] = 0;
                        break;
                }
            }
        }


        float sx = vw_max_x / size_x;
        float sy = vw_max_y / size_y;

        for (int i = 0; i < size_x; i++)
        {
            for (int j = 0; j < size_y; j++)
            {
                var pos = new Vector3(i * sx, j * sy, 0);
                if (walls_h[i, j] > 0)
                    create_wall(pos, sx, (int)VH.H);
                if (walls_v[i, j] > 0)
                    create_wall(pos, sx, (int)VH.V);
            }
        }

        for (int i = 0; i < size_y; i++)
        {
            var pos = new Vector3(i * sx, size_y * sy, 0);
            create_wall(pos, sx, (int)VH.H);
        }


        for (int j = 0; j < size_y; j++)
        {
            var pos = new Vector3(size_x * sx, j * sy, 0);
            create_wall(pos, sx, (int)VH.V);
        }
    }
    */

    List<GameObject> cube = new List<GameObject>();
    //CreatePrimitive
    void create_wall(Vector3 position,float size = 1.0f, int vh=(int)VH.H)
    {
        print(".");
        
/*        if (vh == (int)VH.H)
        {
            cube.transform.localScale = new Vector3(size*0.75f, size / 3.0f, size);
            position.x += size/2;
            cube.transform.position = position;
        }
        else
        {
            cube.transform.localScale = new Vector3(size / 3.0f, size * 0.75f, size);
            position.y += size/2;
            cube.transform.position = position;
        }*/
          

        
    }
    /*
    void maze_alg_binary(NSEW wind_dir)
    {
        bool i_begin = false;
        bool j_begin = false;
        bool i_end = false;
        bool j_end = false;

        for (int i = 0; i < size_x; i++)
        {
            for (int j = 0; j < size_y; j++)
            {
                walls_v[i, j] = 1;
                walls_h[i, j] = 1;

                i_begin = (i == 0);
                j_begin = (j == 0);
                i_end = (i == (size_x-1));
                j_end = (j == (size_y-1));

                int r = GBR.Next(0, 2);
                if(r==1)
                {
                    if ((wind_dir == NSEW.NE) || (wind_dir == NSEW.NW) &&!(j_end))
                        walking_dir[i, j] = DIR.N;
                    if ((wind_dir == NSEW.SE) || (wind_dir == NSEW.SW) &&!(j_begin))
                        walking_dir[i, j] = DIR.S;
                }
                if(r==0)
                {
                    if ((wind_dir == NSEW.NE) || (wind_dir == NSEW.SE))
                        walking_dir[i, j] = DIR.E;
                    if ((wind_dir == NSEW.NW) || (wind_dir == NSEW.SW))
                        walking_dir[i, j] = DIR.W;
                }
            }
        }
    }*/


    void pressed_button()
    {
        dropdown_changed(dropdown);
    }
    void dropdown_changed(Dropdown dropdown)
    {
        string option = dropdown.options[dropdown.value].text;
        print(option);
        switch(option)
        {
            case "NW":
                mc.construct_binary_tree(CORNER.NW);
            break;            
            case "SW":
                mc.construct_binary_tree(CORNER.SW);
            break;            
            case "NE":
                mc.construct_binary_tree(CORNER.NE);
            break;            
            case "SE":
                mc.construct_binary_tree(CORNER.SE);
            break;
        }
        mc.genetate_dijkstra();
        mc.draw_all_walls();
        
    }

    void slider_changed(Slider slider)
    {
        print(slider.value);
        size_x = (int)slider.value;
        size_y = (int)slider.value;
        mc.delete_all();
        mc = new MazeClass.MazeGenerator(size_x, size_y);
        dropdown_changed(dropdown);
    }

    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate {
            dropdown_changed(dropdown);
        });

        slider.onValueChanged.AddListener(delegate {
            slider_changed(slider);
        });

        button.onClick.AddListener(pressed_button);

        mc = new MazeClass.MazeGenerator(size_x,size_y);
        mc.construct_binary_tree(CORNER.NE);
        mc.genetate_dijkstra();
        mc.draw_all_walls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
