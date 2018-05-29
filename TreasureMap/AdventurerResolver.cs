using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreasureMap.Model;

namespace TreasureMap
{
    internal class AdventurerResolver
    {
        #region fields
        Map map;
        List<Adventurer> adventurers;
        #endregion

        internal AdventurerResolver(Map _map, List<Adventurer> _adventurers)
        {
            map = _map;
            adventurers = _adventurers;
        }


        internal void Resolve()
        {
            //on récupére le nombre maximum d'action d'un aventurier de la simu, afin de préparer le nombre de boucle
            int maxActionCount = adventurers.Select(a => a.MoveQueue.Count).Max();


            //on prépare la simu
            for (int i = 0; i < maxActionCount; i++)
            {
                //Pour un tour de simu
                foreach (Adventurer currentAdventurer in adventurers)
                {
                    //Pour chaque aventurier
                    //On détermine l'action
                    ComputeAdventurerMove(currentAdventurer);
                }
                // Peut etre chamboulé en cas d'action illégale donc on recalcule
                if (adventurers.Select(a => a.MoveQueue.Count).Max() == 0)
                    return;
            }

        }


        #region private
        //calcul juste l'orientation et l'arrivée du point
        private void ComputeAdventurerMove(Adventurer adventurer)
        {
            

            // on mets en place une boucle au cas ou la consigne est de dépiler les moves tant qu'ils sont illégaux
            // sinon ignorer la boucle
            bool keepmoving = true;
            while (keepmoving)
            {
                if (adventurer.MoveQueue.Count == 0)
                    return;
                Move move = adventurer.MoveQueue.Dequeue();
                switch (move)
                {
                    case Move.Front:
                        if (TryMoveFront(adventurer));
                        keepmoving = false;
                        break;
                    case Move.Left:
                    case Move.Right:
                        Rotate(adventurer, move);
                        keepmoving = false;
                        break;
                    default:
                        throw new FormatException();

                }
            }

        }

        private void Rotate(Adventurer adventurer, Move move)
        {

            //Pour la rotation on va exploiter les valeurs de l'énumération ce sera moins lourd 
            if (move == Move.Right)
            {
                adventurer.Orientation = (Orientation)((((int)adventurer.Orientation) + 1) % 4);
            }
            else if (move == Move.Left)
            {
                if (adventurer.Orientation == Orientation.Nord)
                {
                    adventurer.Orientation = Orientation.Ouest;
                }
                else
                {
                    adventurer.Orientation = (Orientation)Math.Abs((((int)adventurer.Orientation) - 1) % 4);

                }
            }
            else
            { throw new FormatException(); }


        }

        private bool TryMoveFront(Adventurer adventurer)
        {

            var orientation = adventurer.Orientation;

            //on récupére la position de l'aventurier et on essaye de voir si la destination est valide
            Point destination = adventurer.Position;
            switch (orientation)
            {
                case Orientation.Nord:
                    destination.Y = destination.Y - 1;
                    break;
                case Orientation.Sud:
                    destination.Y = destination.Y + 1;
                    break;
                case Orientation.Ouest:
                    destination.X = destination.X - 1;
                    break;
                case Orientation.Est:
                    destination.X = destination.X + 1;
                    break;
            }

            //Si impossible de bouger, rien ne se passe ce tour ci
            if (!IsMoveValid(destination))
            {
                return false;
            }

            //Sinon, on move et on récupère un trésor si il y en a un
            adventurer.Position = destination;
            for (int j = 0; j < map.Treasures.Count; j++)
            {
                if (map.Treasures[j].Position.Equals(destination) && map.Treasures[j].Number > 0)
                {
                    var temptrez = map.Treasures[j];
                    temptrez.Number--;
                    map.Treasures[j] = temptrez;
                    adventurer.Treasure++;
                }
            }

            //ON nettoie la liste des trésors si un trésor a atteint 0 

            map.Treasures.RemoveAll(i => i.Number <= 0);

            return true;
        }

        private bool IsMoveValid(Point destination)
        {
            //On vérifie qu'on soit encore sur la carte
            if (destination.X + 1 > map.Whidth || destination.X + 1 <= 0 || destination.Y + 1 > map.Height || destination.Y + 1 <= 0)
                return false;

            //On check si des montagnes sont sur le chemin
            foreach (Point mountain in map.Moutains)
            {
                if (mountain.Equals(destination))
                    return false;
            }

            //on check si d'autres aventuriers sont pas dans le coin
            foreach (Adventurer adv in adventurers)
            {
                if (adv.Position.Equals(destination))
                    return false;
            }

            return true;
        }

        #endregion
    }
}
