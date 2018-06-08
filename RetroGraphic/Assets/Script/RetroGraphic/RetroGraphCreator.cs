

namespace Retro
{
    public enum eGraphType
    {
        GPU,
        Texture,
    }


    public class RetroGraphCreator
    {
        /// <summary>
        /// 初始化绘图底层
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IRetroGraph InitGraph(int width, int height, eGraphType type)
        {
            IRetroGraph graph = null;

            if(type == eGraphType.Texture)
            {
                graph = TextureGraph.Create(width, height);
            }
            else
            {
                //TODO 
            }

            return graph;
        }
    }
}