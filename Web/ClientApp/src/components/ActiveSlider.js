import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Link } from 'react-router-dom';
import { fetchBlogData } from '../redux/slice/blogSlice'
import { Swiper, SwiperSlide } from 'swiper/react'
import 'swiper/css'
import 'swiper/css/pagination'
import { Pagination } from 'swiper/modules'
import "../swiperstyle.css"

const ActiveSlider = () => {
    const blog = useSelector(state => state.blog);
    const dispatch = useDispatch();
    useEffect(() => {
        dispatch(fetchBlogData())

    },[]);

    return (
        <main>
            <div className="container">
                {blog.loading && <div>loading...</div>}
                {!blog.loading && blog.error ? <div> {blog.error}</div> : null}
                {!blog.loading && blog.data.lists !== undefined ?
                    <Swiper
                        modules={[Pagination]}
                        grabCursor={false}
                        initialSlide={1}
                        centeredSlides={true}
                        slidesPerView="auto"
                        speed={800}
                        slideToClickedSlide={true}
                        pagination={{ el: ".swiper-pagination", clickable: true }}
                        breakpoints={{
                            320: { spaceBetween: 40 },
                            430: { spaceBetween: 50 },
                            500: { spaceBetween: 70 },
                            650: { spaceBetween: 30 }
                        }}
                    >
                        {blog.data.lists.map((x) =>
                        (
                            <SwiperSlide className="swiper-slide" key={x.id} style={{
                                backgroundImage: `url('data:image/png;base64,${x.image}')`,
                                backgroundSize: 'contain',
                                backgroundPosition: 'center'
                            }}>

                                <div className="title">
                                    <h1>{x.title}</h1>
                                </div>
                                <div className="content">
                                    <div className="score">4.5</div>
                                    <Link className="text" to={"/blog/" + x.id} style={{ cursor: "grab" }}>
                                        <h2>{x.title}</h2>
                                        <p>
                                            {x.content.substring(0, 50)}...
                                        </p>
                                    </Link>
                                    <div className="genre">
                                        <span style={{ "--i": 1 }}>Supernatural</span>
                                        <span style={{ "--i": 2 }}>Comedy</span>
                                        <span style={{ "--i": 3 }}>Mystery</span>
                                    </div>
                                </div>
                            </SwiperSlide>
                        ))}
                        <div className="swiper-pagination"></div>
                    </Swiper>
                    : null}
            </div>
        </main>
    )
}

export default ActiveSlider