using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class MazeClass : MonoBehaviour
{


    public class MazeGenerator
    {
        helper helper = new helper();
        public System.Random RNG = new System.Random();

        private GameObject[,] wall_primitives_v;
        private GameObject[,] wall_primitives_h;
        private GameObject[,] floor_primitives;

        private int[,] wall_is_open_h;
        private int[,] wall_is_open_v;

        int wx;
        int wy;

        int[,] dijkstra;
        DIR[,] walking_dir;

        public void delete_all()
        {
            print("DESTROY");
            for (int i = 0; i < wx; i++)
            {
                for (int j = 0; j < wy; j++)
                {

                    Destroy(wall_primitives_h[i, j]);
                    Destroy(wall_primitives_v[i, j]);
                    Destroy(floor_primitives[i, j]);

                }
            }
                    
           
        }
        public MazeGenerator(int _wx, int _wy)
        {
            wx = _wx;
            wy = _wy;

            wall_primitives_v = new GameObject[wx + 1, wy + 1];
            wall_primitives_h = new GameObject[wx + 1, wy + 1];
            floor_primitives = new GameObject[wx + 1, wy + 1];
            dijkstra = new int[wx, wy];

            wall_is_open_h = new int[wx + 1, wy + 1];
            wall_is_open_v = new int[wx + 1, wy + 1];
            walking_dir = new DIR[wx + 1, wy + 1];

            int n_walls = wx * wy * 2;

            float size = 10.0f / wx;

            for (int i = 0, n = 0; i < wx; i++)
            {
                for (int j = 0; j < wx; j++, n++)
                {
                    wall_primitives_h[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall_primitives_v[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    floor_primitives[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    wall_primitives_h[i, j].transform.localScale = new Vector3(size * 0.75f, size / 4.0f, size);
                    wall_primitives_v[i, j].transform.localScale = new Vector3(size / 4.0f, size * 0.75f, size);
                    floor_primitives[i, j].transform.localScale = new Vector3(size, size, size * 0.05f);

                    wall_primitives_h[i, j].transform.position = new Vector3(i * size + size / 2, j * size, 0);
                    wall_primitives_v[i, j].transform.position = new Vector3(i * size, j * size + size / 2, 0);
                    floor_primitives[i, j].transform.position = new Vector3(i * size + size / 2, j * size + size / 2, 0);
                }
            }
            for (int k = 0; k < wx; k++)
            {
                wall_primitives_v[wx, k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall_primitives_v[wx, k].transform.localScale = new Vector3(size / 6.0f, size * 0.75f, size);
                wall_primitives_v[wx, k].transform.position = new Vector3(wx * size, k * size + size / 2, 0);
            }

            for (int k = 0; k < wy; k++)
            {
                wall_primitives_h[k, wx] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wall_primitives_h[k, wx].transform.localScale = new Vector3(size * 0.75f, size / 6.0f, size);
                wall_primitives_h[k, wx].transform.position = new Vector3(k * size + size / 2, wx * size, 0);
            }
        }

        private void set_all_active(bool active = true)
        {
            for (int i = 0, n = 0; i < wx; i++)
            {
                for (int j = 0; j < wx; j++, n++)
                {
                    wall_primitives_h[i, j].SetActive(active);
                    wall_primitives_v[i, j].SetActive(active);
                }
            }
        }

        private DIR choose_direction(DIR a, DIR b)
        {
            int rnd = RNG.Next(0, 2000) % 2;
            if (rnd == 0)
                return a;
            else
                return b;
        }
        public void construct_binary_tree(CORNER c = CORNER.NE)
        {
            set_all_active(true);
            DIR d1; DIR d2;
            int c1; int c2;

            d1 = DIR.X; d2 = DIR.X; c1 = -99; c2 = -99;

            if (c == CORNER.NE) { d1 = DIR.S; d2 = DIR.W; c1 = 0; c2 = 0; }
            if (c == CORNER.NW) { d1 = DIR.S; d2 = DIR.E; c1 = wy - 1; c2 = 0; }
            if (c == CORNER.SE) { d1 = DIR.N; d2 = DIR.W; c1 = 0; c2 = wx - 1; }
            if (c == CORNER.SW) { d1 = DIR.N; d2 = DIR.E; c1 = wy - 1; c2 = wx - 1; }


            for (int i = 0; i < wx; i++)
            {
                for (int j = 0; j < wy; j++)
                {

                    if (i == c1)
                        walking_dir[i, j] = d1;
                    else if (j == c2)
                        walking_dir[i, j] = d2;
                    else
                        walking_dir[i, j] = choose_direction(d1, d2);

                }
            }
            walking_dir_to_walls();
        }

        private void walking_dir_to_walls()
        {

            for (int i = 0; i < wx; i++)
            {
                for (int j = 0; j < wy; j++)
                {
                    wall_is_open_h[i, j] = 0;
                    wall_is_open_v[i, j] = 0;
                }
            }
            for (int i = 0; i < wx; i++)
            {
                for (int j = 0; j < wy; j++)
                {
                    int ip = i + 1;
                    int jp = j + 1;

                    switch (walking_dir[i, j])
                    {
                        case DIR.N:
                            wall_is_open_h[i, jp] = 1;
                            break;
                        case DIR.S:
                            wall_is_open_h[i, j] = 1;
                            break;
                        case DIR.E:
                            wall_is_open_v[ip, j] = 1;
                            break;
                        case DIR.W:
                            wall_is_open_v[i, j] = 1;
                            break;
                    }
                }
            }
        }
        
        public void genetate_dijkstra()
        {

            for (int i = 0; i < wx - 0; i++)
            {
                for (int j = 0; j < wy - 0; j++)
                {
                    dijkstra[i,j] = 0;
                }
            }
                    bool cont = true;
            dijkstra[8,8] = 1;
            int n = 25;
            while (cont)
            {
                cont = false;
                for (int i = 0; i < wx - 0; i++)
                {
                    for (int j = 0; j < wy - 0; j++)
                    {
                        if(i<wx-1)
                        if ((wall_is_open_v[i + 1, j] > 0) && (dijkstra[i, j] != 0) && (dijkstra[i+1, j] == 0))
                        {
                            dijkstra[i + 1, j] = dijkstra[i, j] + 1; cont = true;
                        }
                        if(i>0)
                        if ((wall_is_open_v[i, j] > 0) && (dijkstra[i, j] != 0) && (dijkstra[i-1, j] == 0))
                        {
                            dijkstra[i-1, j] = dijkstra[i, j] + 1; cont = true;
                        }
                        if(j<wy-1)
                        if ((wall_is_open_h[i, j+1] > 0) && (dijkstra[i, j] != 0) && (dijkstra[i, j+1] == 0))
                        {
                            dijkstra[i, j+1] = dijkstra[i, j] + 1; cont = true;
                        }
                        if(j>0)
                        if ((wall_is_open_h[i, j] > 0) && (dijkstra[i, j] != 0) && (dijkstra[i, j-1] == 0))
                        {
                            dijkstra[i, j-1] = dijkstra[i, j] + 1; cont = true;
                        }

                    }
                }
            }

            for (int i = 0; i < wx; i++)
            {
                for (int j = 0; j < wy; j++)
                {
                    var col = helper.dijkstra_to_color(dijkstra[i, j]);
                    floor_primitives[i, j].GetComponent<Renderer>().material.color = col;
                }
            }

            //int max = dijkstra.Cast<int>().Max();
            IEnumerable<int> allValues = dijkstra.Cast<int>();
            var mehs = allValues.ToArray().GetValue(1);
            int mm = allValues.Min();
            int xx = allValues.Max();

            print(mehs);
        }

        public void draw_all_walls()
        {
            for (int i = 0; i < wx; i++)
            {
                for (int j = 0; j < wy; j++)
                {
                    if (wall_is_open_h[i, j] > 0)
                        wall_primitives_h[i, j].SetActive(false);

                    if (wall_is_open_v[i, j] > 0)
                        wall_primitives_v[i, j].SetActive(false);
                }
            }
        }

        public void draw_wall(ref GameObject cube, Vector3 position, float size = 1.0f, int vh = (int)VH.H)
        {
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            if (vh == (int)VH.H)
            {
                cube.transform.localScale = new Vector3(size * 0.75f, size / 3.0f, size);
                position.x += size / 2;
                cube.transform.position = position;
            }
            else
            {
                cube.transform.localScale = new Vector3(size / 3.0f, size * 0.75f, size);
                position.y += size / 2;
                cube.transform.position = position;
            }
        }
    }
}
