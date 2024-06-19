import { ShoppingCart } from "@mui/icons-material";
import { AppBar, Badge, Box, IconButton, List, ListItem, Switch, Toolbar, Typography } from "@mui/material";
import { NavLink } from "react-router-dom";

const midLink = [
    //{title: "Home", path: "/"},
    { title: "Catalog", path: "/catalog" },
    { title: "About", path: "/about" },
    { title: "Contact", path: "/contact" },

]
const rightLink = [
    { title: "Login", path: "/login" },
    { title: "Register", path: "/register" },

]

const navStyles = {

    color: 'inherit',
    textDecoration: 'none',
    typography: 'h6',
    '&:hover': {
        color: 'grey.500'
    },
    '&.active': {
        color: 'text.secondary'
    }

}


interface Props {
    darkMode: boolean;
    handleThemeChange: () => void;
}

export default function Header({ darkMode, handleThemeChange }: Props) {

    return (
        <AppBar position="static" sx={{ mb: 4 }}>
            <Toolbar sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>


                <Box display='flex' alignItems='center'>
                    <Typography variant="h6" 
                    component={NavLink} 
                    to='/'
                    sx={navStyles}
                    >
                        Re-Store
                    </Typography>

                    <Switch checked={darkMode} onChange={handleThemeChange} />
                </Box>


                <List sx={{ display: 'flex' }}>
                    {midLink.map(({ title, path }) => (
                        <ListItem
                            component={NavLink}
                            to={path}
                            key={path}
                            sx={navStyles}
                        >
                            {title.toUpperCase()}
                        </ListItem>
                    ))}
                </List>


                <Box display='flex' alignItems='center'>
                    <IconButton size="large" edge="start" color="inherit" sx={{ mg: 2 }} >
                        <Badge badgeContent='4' color="secondary">
                            <ShoppingCart></ShoppingCart>
                        </Badge>
                    </IconButton>
                    <List sx={{ display: 'flex' }}>
                        {rightLink.map(({ title, path }) => (
                            <ListItem
                                component={NavLink}
                                to={path}
                                key={path}
                                sx={navStyles}
                            >
                                {title.toUpperCase()}
                            </ListItem>
                        ))}
                    </List>

                </Box>

            </Toolbar>
        </AppBar>
    )
}