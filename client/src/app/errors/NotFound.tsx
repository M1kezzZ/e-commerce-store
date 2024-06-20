import { Button, Container, Divider, Paper, Typography } from "@mui/material";
import { Link } from "react-router-dom";

export default function NotFound() {
    return (
        <Container component={Paper} sx={{height:400}}>
            <Typography variant="h3" gutterBottom>
                Oops - 404 - Not Found
            </Typography>
            <Divider />
            <Button fullWidth component={Link} to='/catalog'>go back to shop</Button>
        </Container>
    )
}