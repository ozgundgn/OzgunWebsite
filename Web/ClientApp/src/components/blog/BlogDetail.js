import { useParams } from 'react-router-dom';
import { useSelector } from 'react-redux';

const BlogDetail = () => {
    const htmlString = '<h1>Hello, <em>World!</em></h1><p>This is rendered HTML.</p>';
    const { id } = useParams();
    const item = useSelector((state) => state.blog.data.lists.find((x) => x.id === parseInt(id)));
    return (

        <div>
            <h1>Detail Page</h1>
            {item ? item.blogParts.map((b) => (
                <>
                    <div dangerouslySetInnerHTML={{ __html: b.text }} />
                    <br />
                    {b.codeBlocks ? b.codeBlocks.map((c) => (
                        <p>{c.code}</p>
                    )) : (<br />)}
                    <br />
                    {b.images ? b.images.map((im) => (
                        <img
                            src={im.imageInfo}
                            alt={item.imageInfo}
                            style={{ width: '100px', height: '100px', objectFit: 'cover' }}
                        />
                    )) : (<br />)}
                </>

            )) : (
                <p>Item not found</p>
            )}

        </div>
    )
}

export default BlogDetail;