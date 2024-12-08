import { useParams } from 'react-router-dom';
export function Description() {
    const { title } = useParams();

    return (
        <h1>Desscription Detail Page  "" {title} ""</h1>
    );
}