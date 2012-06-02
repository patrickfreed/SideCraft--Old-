using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SideCraft.entity {
   public interface Entity {
       void update();

       void draw();

       void destroy();

       Location getLocation();

       Rectangle getBounds();
    }
}
