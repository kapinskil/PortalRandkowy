import { Photo } from './Photo';

export interface User {

    /** Base information */
    id: number;
    username: string;
    gender: string;
    age: number;
    zodiacSign: string;
    created: Date;
    lastActive: Date;
    city: string;
    country: string;
    /** overlap info */
    growth: string;
    eyeColor: string;
    hairColor: string;
    martialStatus: string;
    education: string;
    profession: string;
    children: string;
    languages: string;
    /** About me */
    motto: string;
    description: string;
    personality: string;
    lookingFor: string;
    /** hobby */
    interests: string;
    freeTime: string;
    sport: string;
    movies: string;
    music: string;
    /** preferences */
    iLike: string;
    idoNotLike: string;
    makesMeLaugh: string;
    itFeelsBestIn: string;
    friendeWouldDescribeMe: string;
    /** overlap photo */
    photos: Photo[];
    photoUrl: string;
}
