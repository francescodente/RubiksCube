﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class Cube : IEnumerable<Cubie>
    {
        const int DIMENSION = 3;

        private List<Cubie> _cubies;

        internal RubiksColor?[, ,] Solved { get; private set; }

        public IEnumerable<Cubie> Centers
        {
            get
            {
                return _cubies.Centers();
            }
        }

        public IEnumerable<Cubie> Edges
        {
            get
            {
                return _cubies.Edges();
            }
        }

        public IEnumerable<Cubie> Corners
        {
            get
            {
                return _cubies.Corners();
            }
        }

        public Cube()
        {
            _cubies = new List<Cubie>();

            Solved = new RubiksColor?[DIMENSION + 2, DIMENSION + 2, DIMENSION + 2];

            /*
             * 
             *           Green                     
             *          ________                     z
             *         /       /|                   /
             *        / White / |                  /_____
             *       /_______/  |                  |     x
             * Red > |       |  / < Orange         |
             *       | Blue  | /                   y
             *       |_______|/
             *           ^
             *         Yellow
             */

            for (int i = 1; i <= DIMENSION; i++)
                for (int j = 1; j <= DIMENSION; j++)
                {
                    Solved[i, 0, j] = RubiksColor.White;
                    Solved[i, j, 0] = RubiksColor.Blue;
                    Solved[0, i, j] = RubiksColor.Red;
                    Solved[i, DIMENSION + 1, j] = RubiksColor.Yellow;
                    Solved[i, j, DIMENSION + 1] = RubiksColor.Green;
                    Solved[DIMENSION + 1, i, j] = RubiksColor.Orange;
                }

            for (int x = 0; x < DIMENSION; x++)
                for (int y = 0; y < DIMENSION; y++)
                    for (int z = 0; z < DIMENSION; z++)
                        _cubies.Add(new Cubie(x, y, z, this));
        }

        internal void RotateCubiesAroundAxis(IEnumerable<Cubie> cubies, Axis axis, bool reverse)
        {
            foreach (Cubie c in cubies)
            {
                int newX = c.X, newY = c.Y, newZ = c.Z;

                if (reverse)
                    switch (axis)
                    {
                        case Axis.Z:
                            newX = DIMENSION - 1 - c.Y;
                            newY = c.X;
                            break;
                        case Axis.X:
                            newZ = DIMENSION - 1 - c.Y;
                            newY = c.Z;
                            break;
                        case Axis.Y:
                            newX = DIMENSION - 1 - c.Z;
                            newZ = c.X;
                            break;
                    }
                else
                    switch (axis)
                    {
                        case Axis.Z:
                            newY = DIMENSION - 1 - c.X;
                            newX = c.Y;
                            break;
                        case Axis.X:
                            newY = DIMENSION - 1 - c.Z;
                            newZ = c.Y;
                            break;
                        case Axis.Y:
                            newZ = DIMENSION - 1 - c.X;
                            newX = c.Z;
                            break;
                    }

                c.X = newX;
                c.Y = newY;
                c.Z = newZ;

                c.Rotate(axis, reverse);
            }
        }

        internal void RotateFace(Face face, bool reverse)
        {
            if (face == Face.Front || face == Face.Back)
                RotateCubiesAroundAxis(GetFaceCubies(face), Axis.Z, reverse);
            else if (face == Face.Left || face == Face.Right)
                RotateCubiesAroundAxis(GetFaceCubies(face), Axis.X, reverse);
            else
                RotateCubiesAroundAxis(GetFaceCubies(face), Axis.Y, reverse);
        }

        public Cube Clone()
        {
            Cube clone = new Cube();

            clone._cubies.Clear();

            foreach (Cubie c in _cubies)
                clone._cubies.Add(c.Clone());

            return clone;
        }

        public void RotateFace(Move m)
        {
            Face f = (Face)Math.Abs((int)m);

            RotateFace(f, (int)m > 0);
        }

        public IEnumerable<Cubie> GetFaceCubies(Face face)
        {
            foreach (Cubie c in _cubies)
                if (FaceContains(face, c))
                    yield return c;
        }

        public RubiksColor GetFaceColor(Face face)
        {
            return (RubiksColor)GetFaceCubies(face).Centers().ElementAt<Cubie>(0).GetColor(face);
        }

        public bool FaceContains(Face face, Cubie c)
        {
            switch (face)
            {
                case Face.Front:
                    if (c.Z == 0)
                        return true;
                    return false;
                case Face.Back:
                    if (c.Z == DIMENSION - 1)
                        return true;
                    return false;
                case Face.Right:
                    if (c.X == DIMENSION - 1)
                        return true;
                    return false;
                case Face.Left:
                    if (c.X == 0)
                        return true;
                    return false;
                case Face.Up:
                    if (c.Y == 0)
                        return true;
                    return false;
                case Face.Down:
                    if (c.Y == DIMENSION - 1)
                        return true;
                    return false;
                default:
                    return false;
            }
        }

        public IEnumerator<Cubie> GetEnumerator()
        {
            foreach (Cubie c in _cubies)
                yield return c;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Cubie FindCenter(RubiksColor color)
        {
            foreach (Cubie c in Centers)
                if (c.Colors.Contains(color))
                    return c;

            return null;
        }

        public bool IsCubiePlacedCorrectly(Cubie c)
        {
            foreach (Face f in Enum.GetValues(typeof(Face)))
                if (c.GetColor(f) != null && c.GetColor(f) != GetFaceColor(f))
                    return false;
            return true;
        }
            
        public Cubie FindEdge(RubiksColor color1, RubiksColor color2)
        {
            foreach (Cubie c in Edges)
                if (c.Colors.Contains(color1) && c.Colors.Contains(color2))
                    return c;

            return null;
        }

        public Cubie FindCorner(RubiksColor color1, RubiksColor color2, RubiksColor color3)
        {
            foreach (Cubie c in Corners)
                if (c.Colors.Contains(color1) && c.Colors.Contains(color2) && c.Colors.Contains(color3))
                    return c;

            return null;
        }

        public void SetView(RubiksColor front, RubiksColor up)
        {
            Cubie center = FindCenter(front);


        }

        public void SetView(CubeView view)
        {
            SetView(view.FrontColor, view.UpColor);
        }

        public Cubie FindCubie(int x, int y, int z)
		{
			foreach (Cubie c in _cubies)
				if (c.X == x && c.Y == y && c.Z == z)
					return c;
				
			return null;
		}

        public bool IsSolved()
        {
            foreach (Cubie c in _cubies)
                if (!IsCubiePlacedCorrectly(c))
                    return false;

            return true;
        }

        public void Scramble(int n)
        {
            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                Face f = (Face)rnd.Next(1, 7);
                bool rev = rnd.Next(0, 2) == 1;

                RotateFace(f, rev);
            }
        }
    }
}
