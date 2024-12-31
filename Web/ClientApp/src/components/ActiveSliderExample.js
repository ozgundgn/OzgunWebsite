import React from 'react'
import { Swiper, SwiperSlide } from 'swiper/react'
import 'swiper/css'
import 'swiper/css/pagination'
import { Pagination } from 'swiper/modules'
import "../style.css"

const ActiveSlider = () => {
    return (
        <main>
            <div className="container">
                <Swiper
                    modules={[ Pagination ]}
                    grabCursor={true}
                    initialSlide={0}
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
                    <SwiperSlide className="swiper-slide slide-1">
                        <div className="title">
                            <h1>The Bear</h1>
                        </div>
                        <div className="content">
                            <div className="score">8.6</div>
                            <div className="text">
                                <h2>The Bear</h2>
                                <p>
                                    A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a
                                    A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a A  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a

                                </p>
                            </div>
                            <div className="genre">
                                <span style={{ "--i": 1 }}>Drama</span>
                                <span style={{ "--i": 2 }}>Comedy</span>
                            </div>
                        </div>
                    </SwiperSlide>
                    <SwiperSlide className="swiper-slide slide-2">
                        <div className="title">
                            <h1>Wednesday</h1>
                        </div>
                        <div className="content">
                            <div className="score">4.5</div>
                            <div className="text">
                                <h2>Wednesday</h2>
                                <p>
                                    A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a
                                </p>
                            </div>
                            <div className="genre">
                                <span style={{ "--i": 1 }}>Supernatural</span>
                                <span style={{ "--i": 2 }}>Comedy</span>
                                <span style={{ "--i": 3 }}>Mystery</span>
                            </div>
                        </div>
                    </SwiperSlide>
                    <SwiperSlide className="swiper-slide slide-3">
                        <div className="title">
                            <h1>Severance</h1>
                        </div>
                        <div className="content">
                            <div className="score">7.3</div>
                            <div className="text">
                                <h2>Severance</h2>
                                <p>
                                    A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a
                                </p>
                            </div>
                            <div className="genre">
                                <span style={{ "--i": 1 }}>Psychological Thriller</span>
                                <span style={{ "--i": 2 }}>Science Fiction</span>
                            </div>
                        </div>
                    </SwiperSlide>
                    <SwiperSlide className="swiper-slide slide-4">
                        <div className="title">
                            <h1>Game of Thrones</h1>
                        </div>
                        <div className="content">
                            <div className="score">9.2</div>
                            <div className="text">
                                <h2>Game of Thrones</h2>
                                <p>
                                    A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a
                                </p>
                            </div>
                            <div className="genre">
                                <span style={{ "--i": 1 }}>Fantasy</span>
                                <span style={{ "--i": 2 }}>Drama</span>
                                <span style={{ "--i": 3 }}>Adventure</span>

                            </div>
                        </div>
                    </SwiperSlide>
                    <SwiperSlide className="swiper-slide slide-5">
                        <div className="title">
                            <h1>Succession</h1>
                        </div>
                        <div className="content">
                            <div className="score">8.9</div>
                            <div className="text">
                                <h2>Succession</h2>
                                <p>
                                    A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a  A drama series that dives into world of a
                                </p>
                            </div>
                            <div className="genre">
                                <span style={{ "--i": 1 }}>Drama</span>
                                <span style={{ "--i": 2 }}>Satire</span>
                            </div>
                        </div>
                    </SwiperSlide>
                    <div className="swiper-pagination"></div>
                </Swiper>
            </div>
        </main>
    )
}

export default ActiveSlider